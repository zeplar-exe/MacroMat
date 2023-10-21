using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

using MacroMat.Common;
using MacroMat.Input;

namespace MacroMat.SystemCalls.Windows;

// https://stackoverflow.com/questions/604410/global-keyboard-capture-in-c-sharp-application
// Black magic in a nutshell.

//Based on https://gist.github.com/Stasonix
internal class WindowsHook : IPlatformHook, IKeyboardHook, IMouseHook
{
    // ReSharper disable once InconsistentNaming
    public const int WH_KEYBOARD_LL = 13;
    // ReSharper disable once InconsistentNaming
    public const int WH_MOUSE_LL = 14;
    
    private IntPtr User32LibraryHandle { get; set; }
    
    private List<(int HookCode, Win32.HookProc Proc)> Hooks { get; }
    private List<IntPtr> HookHandles { get; }
    
    private MessageReporter Reporter { get; }
    
    public event KeyboardHookCallback? OnKeyEvent;
    public event MouseHookCallback? OnMouseEvent;

    public WindowsHook(MessageReporter reporter)
    {
        Hooks = new List<(int HookCode, Win32.HookProc Proc)>();
        HookHandles = new List<IntPtr>();
        
        Reporter = reporter;
        User32LibraryHandle = IntPtr.Zero;

        Hooks.Add((WH_KEYBOARD_LL, LowLevelKeyboardProc));
        Hooks.Add((WH_MOUSE_LL, LowLevelMouseProc));
        // keep KeyboardHookRef, MouseHookRef alive, GC is not aware about SetWindowsHookEx behaviour.

        User32LibraryHandle = Win32.LoadLibrary("User32");

        if (User32LibraryHandle == IntPtr.Zero)
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
            var handle = Win32.SetWindowsHookEx(hook.HookCode, hook.Proc, User32LibraryHandle, 0);

            if (handle == IntPtr.Zero)
            {
                WindowsHelper.HandleError(e =>
                    Reporter.Log(
                        $"Failed to adjust hook ({hook.HookCode}): " +
                        $"[{e.NativeErrorCode}] {e.Message}."));
            }
            
            HookHandles.Add(handle);
        }

        return true;
    }

    public IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam)
    {
        var wparamTyped = wParam.ToInt32();

        if (Enum.IsDefined(typeof(WindowsKeyboardState), wparamTyped))
        {
            var keyboardData = (Win32.KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(Win32.KBDLLHOOKSTRUCT))!;
            var keyboardState = (WindowsKeyboardState)wparamTyped;
            
            const int injectedBit = 4;

            var flags = new BitArray(new[] { keyboardData.Flags });
            var isInjected = flags.Count > injectedBit && flags.Get(injectedBit);
            
            var inputType = keyboardState switch
            {
                WindowsKeyboardState.KeyDown or WindowsKeyboardState.SysKeyDown => KeyInputType.KeyDown,
                WindowsKeyboardState.KeyUp or WindowsKeyboardState.SysKeyUp => KeyInputType.KeyUp,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            var eventData = new KeyboardEventData(
                Scancode.From((short)keyboardData.HardwareScanCode),
                VirtualKey.From(keyboardData.VirtualCode),
                inputType, isInjected,
                keyboardState is WindowsKeyboardState.SysKeyDown);

            var args = new KeyboardEventArgs(eventData);
            
            OnKeyEvent?.Invoke(this, args);
            
            return args.Handled ? IntPtr.MaxValue : Win32.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }

        return Win32.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
    }

    public IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam)
    {
        var wparamTyped = wParam.ToInt32();

        if (Enum.IsDefined(typeof(WindowsMouseState), wparamTyped))
        {
            var mouseData = (Win32.MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(Win32.MSLLHOOKSTRUCT))!;
            var mouseState = (WindowsMouseState)wparamTyped;

            if (mouseState == WindowsMouseState.MouseMove)
            {
                return IntPtr.Zero;
            }

            var button = mouseState switch
            {
                WindowsMouseState.LButtonDown => MouseButton.Left,
                WindowsMouseState.LButtonUp => MouseButton.Left,
                WindowsMouseState.RButtonDown => MouseButton.Right,
                WindowsMouseState.RButtonUp => MouseButton.Right,
                WindowsMouseState.MouseWheel => MouseButton.MouseWheel
            };

            var inputType = mouseState switch
            {
                WindowsMouseState.LButtonDown or WindowsMouseState.RButtonDown => MouseInputType.Down,
                WindowsMouseState.LButtonUp or WindowsMouseState.RButtonUp => MouseInputType.Up,
                WindowsMouseState.MouseWheel =>
                    mouseData.mouseData >> 16 < 0 ? MouseInputType.WheelBackward : MouseInputType.WheelForward
            };
            
            var eventData = new MouseEventData(button, inputType);
            var args = new MouseEventArgs(eventData);
            
            OnMouseEvent?.Invoke(this, args);
            
            return args.Handled ? IntPtr.MaxValue : Win32.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }

        return Win32.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
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
            foreach (var handle in HookHandles)
            {
                if (handle == IntPtr.Zero)
                    continue;
                
                if (!Win32.UnhookWindowsHookEx(handle))
                {
                    WindowsHelper.HandleError(e =>
                        Reporter.Error(
                            $"Failed to remove keyboard hooks for '{Process.GetCurrentProcess().ProcessName}': " +
                            $"[{e.NativeErrorCode}] {e.Message}."));
                }
            }
            
            HookHandles.Clear();
            Hooks.Clear();
        }

        if (User32LibraryHandle != IntPtr.Zero)
        {
            if (!Win32.FreeLibrary(User32LibraryHandle)) // reduces reference to library by 1.
            {
                var exception = new Win32Exception(Marshal.GetLastWin32Error());

                Reporter.Error($"Failed to unload library 'User32.dll': [{exception.NativeErrorCode}] {exception.Message}.");
            }

            User32LibraryHandle = IntPtr.Zero;
        }
    }
}