using System.Diagnostics;

using MacroMat.Common;
using MacroMat.Input;
using MacroMat.SystemCalls;
using MacroMat.SystemCalls.Windows;

namespace MacroMat;

public sealed class MacroListener : IDisposable
{
    private MessageReporter Reporter { get; }
    
    internal IKeyboardHook? KeyboardHook { get; }
    internal MessageLoop? MessageLoop { get; }

    public MacroListener(MessageReporter reporter)
    {
        Reporter = reporter;
        KeyboardHook = GetKeyboardHook();
        
        MessageLoop = GetMessageLoop(() =>
        {
            if (KeyboardHook?.MessageLoopInit() ?? false)
            {
                KeyboardHook.OnKeyEvent += OnKey;
            }
            else
            {
                Reporter.Error("Failed to initialize KeyboardHook, key events will not be fired.");
            }
        });

        if (MessageLoop == null)
        {
            Reporter.Error("Failed to initialize MessageLoop, no events will be fired..");
        }
    }

    public void Start()
    {
        MessageLoop?.Start(exception => throw exception);
    }

    private static MessageLoop? GetMessageLoop(Action initialAction)
    {
        return new OsSelector<MessageLoop>()
            .OnWindows(() => new WindowsLoop(initialAction))
            .Execute();
    }

    private IKeyboardHook? GetKeyboardHook()
    {
        return new OsSelector<IKeyboardHook>()
            .OnWindows(() => new WindowsHook(Reporter))
            .Execute();
    }

    private void OnKey(IKeyboardHook hook, KeyboardEventData data)
    {
        Reporter.Log(data.ToString());
    }

    public void Dispose()
    {
        MessageLoop?.Stop();
        KeyboardHook?.Dispose();
    }
}