using System.Diagnostics;

using MacroMat.Input;
using MacroMat.SystemCalls;
using MacroMat.SystemCalls.Windows;

namespace MacroMat;

public sealed class MacroListener : IDisposable
{
    internal IKeyboardHook? KeyboardHook { get; }
    internal MessageLoop? MessageLoop { get; }

    public MacroListener()
    {
        KeyboardHook = GetKeyboardHook();
        
        MessageLoop = GetMessageLoop(() =>
        {
            if (KeyboardHook != null)
            {
                KeyboardHook.MessageLoopInit();
                KeyboardHook.OnKeyEvent += OnKey;
            }
            else
            {
                Console.WriteLine("Failed to initialize KeyboardHook, key events will not be fired.");
            }
        });

        if (MessageLoop == null)
        {
            Console.WriteLine("Failed to initialize MessageLoop, no events will be fired..");
        }
    }

    public void Start()
    {
        MessageLoop?.Start(exception => throw exception);
    }

    private static MessageLoop? GetMessageLoop(Action initialAction)
    {
        switch (Environment.OSVersion.Platform)
        {
            case PlatformID.Win32NT:
            {
                return new WindowsLoop(initialAction);
            }
            case PlatformID.Unix:
            {
                return null;
            }
            case PlatformID.MacOSX:
            {
                return null;
            }
            default:
            {
                return null;
            }
        }
    }

    private static IKeyboardHook? GetKeyboardHook()
    {
        switch (Environment.OSVersion.Platform)
        {
            case PlatformID.Win32NT:
            {
                return new WindowsHook();
            }
            case PlatformID.Unix:
            {
                return null;
            }
            case PlatformID.MacOSX:
            {
                return null;
            }
            default:
            {
                return null;
            }
        }
    }

    private void OnKey(IKeyboardHook hook, KeyboardEventData data)
    {
        Console.WriteLine(data);
    }

    public void Dispose()
    {
        MessageLoop.Stop();
        KeyboardHook?.Dispose();
    }
}