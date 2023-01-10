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
    //const int HC_ACTION = 0;
    
    [DllImport("kernel32.dll")]
    private static extern IntPtr LoadLibrary(string lpFileName);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    private static extern bool FreeLibrary(IntPtr hModule);

    /// <summary>
    ///     The SetWindowsHookEx function installs an application-defined hook procedure into a hook chain.
    ///     You would install a hook procedure to monitor the system for certain types of events. These events are
    ///     associated either with a specific thread or with all threads in the same desktop as the calling thread.
    /// </summary>
    /// <param name="idHook">hook type</param>
    /// <param name="lpfn">hook procedure</param>
    /// <param name="hMod">handle to application instance</param>
    /// <param name="dwThreadId">thread identifier</param>
    /// <returns>If the function succeeds, the return value is the handle to the hook procedure.</returns>
    [DllImport("USER32", SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, int dwThreadId);

    /// <summary>
    ///     The UnhookWindowsHookEx function removes a hook procedure installed in a hook chain by the SetWindowsHookEx
    ///     function.
    /// </summary>
    /// <param name="hhk">handle to hook procedure</param>
    /// <returns>If the function succeeds, the return value is true.</returns>
    [DllImport("USER32", SetLastError = true)]
    public static extern bool UnhookWindowsHookEx(IntPtr hHook);

    /// <summary>
    ///     The CallNextHookEx function passes the hook information to the next hook procedure in the current hook chain.
    ///     A hook procedure can call this function either before or after processing the hook information.
    /// </summary>
    /// <param name="hHook">handle to current hook</param>
    /// <param name="code">hook code passed to hook procedure</param>
    /// <param name="wParam">value passed to hook procedure</param>
    /// <param name="lParam">value passed to hook procedure</param>
    /// <returns>If the function succeeds, the return value is true.</returns>
    [DllImport("USER32", SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hHook, int code, IntPtr wParam, IntPtr lParam);
    
    private delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);
    
    private const int KfAltdown = 0x2000;

    public const int WH_KEYBOARD_LL = 13;

    public const int VkSnapshot = 0x2c;
    public const int VkLwin = 0x5b;
    public const int VkRwin = 0x5c;
    public const int VkTab = 0x09;
    public const int VkEscape = 0x18;
    public const int VkControl = 0x11;
    
    public const int LlkhfAltdown = KfAltdown >> 8;
    
    private HookProc? KeyboardHookRef { get; set; }
    private IntPtr User32LibraryHandle { get; set; }
    private IntPtr WindowsHookHandle { get; set; }
    
    public event KeyboardHookCallback? OnKeyEvent;

    public WindowsHook()
    {
        WindowsHookHandle = IntPtr.Zero;
        User32LibraryHandle = IntPtr.Zero;
        KeyboardHookRef = LowLevelKeyboardProc; 
        // keep KeyboardHookRef alive, GC is not aware about SetWindowsHookEx behaviour.

        User32LibraryHandle = LoadLibrary("User32");

        if (User32LibraryHandle == IntPtr.Zero)
        {
            var exception = new Win32Exception(Marshal.GetLastWin32Error());
            
            throw new Win32Exception(exception.NativeErrorCode, 
                $"Failed to load library 'User32.dll'. Error {exception.NativeErrorCode}: {exception.Message}.");
        }
    }

    public void MessageLoopInit()
    {
        if (KeyboardHookRef == null)
            return;
        
        WindowsHookHandle = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookRef, User32LibraryHandle, 0);

        if (WindowsHookHandle == IntPtr.Zero)
        {
            var exception = new Win32Exception(Marshal.GetLastWin32Error());

            throw new Win32Exception(exception.NativeErrorCode, 
                $"Failed to adjust keyboard hooks for '{Process.GetCurrentProcess().ProcessName}'. Error {exception.NativeErrorCode}: {exception.Message}.");
        }
    }

    public IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam)
    {
        var fEatKeyStroke = false;

        var wparamTyped = wParam.ToInt32();

        if (Enum.IsDefined(typeof(KeyboardState), wparamTyped))
        {
            var o = Marshal.PtrToStructure(lParam, typeof(WindowsKeyboardInputEvent));
            var p = (WindowsKeyboardInputEvent)o;

            var args = new GlobalKeyboardHookEventArgs(p, (KeyboardState)wparamTyped);
            var keyboardData = args.KeyboardData;
            var keyboardState = args.KeyboardState;
            const int injectedBit = 4;
            
            var isInjected = (keyboardData.Flags & (1 << injectedBit-1)) != 0;

            var inputType = keyboardState switch
            {
                KeyboardState.KeyDown => KeyInputType.KeyDown,
                KeyboardState.KeyUp => KeyInputType.KeyUp,
                KeyboardState.SysKeyDown => KeyInputType.SysKeyDown,
                KeyboardState.SysKeyUp => KeyInputType.SysKeyUp,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            var eventData = new KeyboardEventData(
                keyboardData.HardwareScanCode,
                keyboardData.VirtualCode,
                inputType, isInjected);
            
            OnKeyEvent?.Invoke(this, eventData);

            fEatKeyStroke = args.Handled;
        }

        return fEatKeyStroke ? (IntPtr)1 : CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
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
                if (!UnhookWindowsHookEx(WindowsHookHandle))
                {
                    var errorCode = Marshal.GetLastWin32Error();

                    throw new Win32Exception(errorCode, $"Failed to remove keyboard hooks for '{Process.GetCurrentProcess().ProcessName}'. Error {errorCode}: {new Win32Exception(Marshal.GetLastWin32Error()).Message}.");
                }

                WindowsHookHandle = IntPtr.Zero;

                // ReSharper disable once DelegateSubtraction
                
                KeyboardHookRef -= LowLevelKeyboardProc;
            }
        }

        if (User32LibraryHandle != IntPtr.Zero)
        {
            if (!FreeLibrary(User32LibraryHandle)) // reduces reference to library by 1.
            {
                var errorCode = Marshal.GetLastWin32Error();

                throw new Win32Exception(errorCode, $"Failed to unload library 'User32.dll'. Error {errorCode}: {new Win32Exception(Marshal.GetLastWin32Error()).Message}.");
            }

            User32LibraryHandle = IntPtr.Zero;
        }
    }
}