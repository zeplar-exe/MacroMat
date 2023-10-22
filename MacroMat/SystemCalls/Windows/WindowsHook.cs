using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;
using MacroMat.Common;
using MacroMat.Input;

namespace MacroMat.SystemCalls.Windows;

// https://stackoverflow.com/questions/604410/global-keyboard-capture-in-c-sharp-application
// Black magic in a nutshell

//Based on https://gist.github.com/Stasonix (2 years ago (as of 2023) at least)
[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
internal class WindowsHook : IPlatformHook, IKeyboardHook, IMouseHook
{
    // ReSharper disable once InconsistentNaming
    public const int WH_KEYBOARD_LL = 13;
    // ReSharper disable once InconsistentNaming
    public const int WH_MOUSE_LL = 14;
    
    private FreeLibrarySafeHandle User32LibraryHandle { get; set; }
    
    private List<(WINDOWS_HOOK_ID Id, HOOKPROC Proc)> Hooks { get; }
    private List<SafeHandle> HookHandles { get; }
    
    private MessageReporter Reporter { get; }
    
    public event KeyboardHookCallback? OnKeyEvent;
    public event MouseHookCallback? OnMouseEvent;

    public WindowsHook(MessageReporter reporter)
    {
        Hooks = new List<(WINDOWS_HOOK_ID Id, HOOKPROC Proc)>();
        HookHandles = new List<SafeHandle>();
        
        Reporter = reporter;

        Hooks.Add((WINDOWS_HOOK_ID.WH_KEYBOARD_LL, LowLevelKeyboardProc));
        Hooks.Add((WINDOWS_HOOK_ID.WH_MOUSE_LL, LowLevelMouseProc));
        // keep KeyboardHookRef, MouseHookRef alive, GC is not aware about SetWindowsHookEx behaviour.

        User32LibraryHandle = PInvoke.LoadLibrary("User32");

        if (User32LibraryHandle.IsInvalid)
        {
            WindowsHelper.HandleError(e => 
                Reporter.Error($"Failed to load library 'User32.dll': [{e.NativeErrorCode}] {e.Message}."));
        }
    }

    public bool MessageLoopInit()
    {
        if (Hooks.Count == 0)
            return false;
        
        foreach (var hook in Hooks)
        {
            var handle = PInvoke.SetWindowsHookEx(hook.Id, hook.Proc, User32LibraryHandle, 0);

            if (handle.IsInvalid)
            {
                WindowsHelper.HandleError(e =>
                    Reporter.Log(
                        $"Failed to adjust hook ({hook.Id}): " +
                        $"[{e.NativeErrorCode}] {e.Message}."));
            }
            
            HookHandles.Add(handle);
        }

        return true;
    }

    public LRESULT LowLevelKeyboardProc(int nCode, WPARAM wParam, LPARAM lParam)
    {
        var wParamTyped = wParam.Value.ToUInt32();

        if (Enum.IsDefined(typeof(WindowsKeyboardState), wParamTyped))
        {
            var keyboardData = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT))!;
            var keyboardState = (WindowsKeyboardState)wParamTyped;
            
            const int injectedBit = 4;

            var flags = new BitArray(new[] { (byte)keyboardData.flags });
            var isInjected = flags.Count > injectedBit && flags.Get(injectedBit);
            
            var inputType = keyboardState switch
            {
                WindowsKeyboardState.KeyDown or WindowsKeyboardState.SysKeyDown => KeyInputType.KeyDown,
                WindowsKeyboardState.KeyUp or WindowsKeyboardState.SysKeyUp => KeyInputType.KeyUp,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            var eventData = new KeyboardEventData(
                Scancode.From((ushort)keyboardData.scanCode),
                VirtualKey.From((byte)keyboardData.vkCode),
                inputType, isInjected,
                keyboardState is WindowsKeyboardState.SysKeyDown);

            var args = new KeyboardEventArgs(eventData);
            
            OnKeyEvent?.Invoke(this, args);
            
            return args.Handled ? 
                new LRESULT(IntPtr.MaxValue) : 
                PInvoke.CallNextHookEx(User32LibraryHandle, nCode, wParam, lParam);
        }

        return PInvoke.CallNextHookEx(User32LibraryHandle, nCode, wParam, lParam);
    }

    public LRESULT LowLevelMouseProc(int nCode, WPARAM wParam, LPARAM lParam)
    {
        var wParamTyped = wParam.Value.ToUInt32();
        
        if (Enum.IsDefined(typeof(WindowsMouseState), wParamTyped))
        {
            var mouseData = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT))!;
            var mouseState = (WindowsMouseState)wParamTyped;

            var positionX = mouseData.pt.X; // supposed to use GET_X_LPARAM, cant generate that yet
            var positionY = mouseData.pt.Y; // supposed to use GET_Y_LPARAM, cant generate that yet

            MouseEventData eventData;
            
            if (mouseState == WindowsMouseState.MouseMove)
            {
                eventData = new MouseMoveEventData(positionX, positionY);
            }
            else if (mouseState == WindowsMouseState.MouseWheel)
            {
                // supposed to use GET_WHEEL_DELTA_WPARAM, cant generate that yet
                var scrollDelta = (short)(wParam >> 16); // high order = delta
                
                eventData = new MouseWheelEventData(positionX, positionY, 0, scrollDelta);
            }
            else if (mouseState == WindowsMouseState.MouseHWheel)
            {
                var scrollDelta = (short)(wParam >> 16); // high order = delta
                
                eventData = new MouseWheelEventData(positionX, positionY, scrollDelta, 0);
            }
            else
            {
                var button = mouseState switch
                {
                    WindowsMouseState.LButtonDown => MouseButton.Left,
                    WindowsMouseState.LButtonUp => MouseButton.Left,
                    WindowsMouseState.RButtonDown => MouseButton.Right,
                    WindowsMouseState.RButtonUp => MouseButton.Right
                };

                var buttonInputType = MouseButtonInputType.Down;

                if (mouseState is WindowsMouseState.LButtonDown or WindowsMouseState.RButtonDown)
                    buttonInputType = MouseButtonInputType.Down;
                else if (mouseState is WindowsMouseState.LButtonUp or WindowsMouseState.RButtonUp)
                    buttonInputType = MouseButtonInputType.Up;

                eventData = new MouseButtonEventData(positionX, positionY, button, buttonInputType);
            }

            var args = new MouseEventArgs(eventData);
            
            OnMouseEvent?.Invoke(this, args);

            return args.Handled ? 
                new LRESULT(IntPtr.MaxValue) : 
                PInvoke.CallNextHookEx(User32LibraryHandle, nCode, wParam, lParam);
        }
        
        return PInvoke.CallNextHookEx(User32LibraryHandle, nCode, wParam, lParam);
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~WindowsHook()
    {
        Dispose(false);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (disposing) // because we can unhook only in the same thread, not in garbage collector thread
        {
            foreach (var handle in HookHandles.Where(handle => !handle.IsInvalid).Where(handle => !PInvoke.UnhookWindowsHookEx(new HHOOK(handle.DangerousGetHandle()))))
            {
                WindowsHelper.HandleError(e =>
                    Reporter.Error(
                        $"Failed to remove keyboard hooks for '{Process.GetCurrentProcess().ProcessName}': " +
                        $"[{e.NativeErrorCode}] {e.Message}."));
            }
            
            HookHandles.Clear();
            Hooks.Clear();
        }

        if (!User32LibraryHandle.IsInvalid)
        {
            if (!PInvoke.FreeLibrary(new HMODULE(User32LibraryHandle.DangerousGetHandle()))) // reduces reference to library by 1.
            {
                var exception = new Win32Exception(Marshal.GetLastWin32Error());

                Reporter.Error($"Failed to unload library 'User32.dll': [{exception.NativeErrorCode}] {exception.Message}.");
            }

            User32LibraryHandle.Dispose();
        }
    }
}