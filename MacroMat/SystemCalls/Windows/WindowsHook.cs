using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

using MacroMat.Input;

namespace MacroMat.SystemCalls.Windows;

// https://stackoverflow.com/questions/604410/global-keyboard-capture-in-c-sharp-application
// Black magic in a nutshell.

//Based on https://gist.github.com/Stasonix
internal class WindowsHook : IKeyboardHook
{
    public const int WH_KEYBOARD_LL = 13;
    
    private Win32.HookProc? KeyboardHookRef { get; set; }
    private IntPtr User32LibraryHandle { get; set; }
    private IntPtr WindowsHookHandle { get; set; }
    
    private MessageReporter Reporter { get; }
    
    public event KeyboardHookCallback? OnKeyEvent;

    public WindowsHook(MessageReporter reporter)
    {
        Reporter = reporter;
        WindowsHookHandle = IntPtr.Zero;
        User32LibraryHandle = IntPtr.Zero;
        KeyboardHookRef = LowLevelKeyboardProc; 
        // keep KeyboardHookRef alive, GC is not aware about SetWindowsHookEx behaviour.

        User32LibraryHandle = Win32.LoadLibrary("User32");

        if (User32LibraryHandle == IntPtr.Zero)
        {
            WindowsHelper.HandleError(e => 
                Reporter.Error($"Failed to load library 'User32.dll': [{e.NativeErrorCode}] {e.Message}."));
        }
    }

    public bool MessageLoopInit()
    {
        if (KeyboardHookRef == null)
            return false;
        
        WindowsHookHandle = Win32.SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookRef, User32LibraryHandle, 0);

        if (WindowsHookHandle == IntPtr.Zero)
        {
            WindowsHelper.HandleError(e =>
                Reporter.Error(
                    $"Failed to adjust keyboard hooks for '{Process.GetCurrentProcess().ProcessName}': " +
                    $"[{e.NativeErrorCode}] {e.Message}."));
            

            return false;
        }

        return true;
    }

    public IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam)
    {
        var wparamTyped = wParam.ToInt32();

        if (Enum.IsDefined(typeof(WindowsKeyboardState), wparamTyped))
        {
            var o = Marshal.PtrToStructure(lParam, typeof(WindowsKeyboardInputEvent));
            
            var keyboardData = (WindowsKeyboardInputEvent)o!;
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
                keyboardData.HardwareScanCode,
                keyboardData.VirtualCode,
                inputType, isInjected,
                keyboardState is WindowsKeyboardState.SysKeyUp or WindowsKeyboardState.SysKeyDown);

            var args = new KeyboardEventArgs(eventData);
            
            OnKeyEvent?.Invoke(this, args);
            
            return args.Handled ? IntPtr.Zero : Win32.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
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
            if (WindowsHookHandle != IntPtr.Zero)
            {
                if (!Win32.UnhookWindowsHookEx(WindowsHookHandle))
                {
                    WindowsHelper.HandleError(e =>
                        Reporter.Error(
                            $"Failed to remove keyboard hooks for '{Process.GetCurrentProcess().ProcessName}': " +
                            $"[{e.NativeErrorCode}] {e.Message}."));
                }

                WindowsHookHandle = IntPtr.Zero;

                // ReSharper disable once DelegateSubtraction
                
                KeyboardHookRef -= LowLevelKeyboardProc;
            }
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