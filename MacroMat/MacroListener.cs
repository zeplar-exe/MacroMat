using MacroMat.Common;
using MacroMat.SystemCalls;
using MacroMat.SystemCalls.Windows;

namespace MacroMat;

internal class MacroListener : IDisposable
{
    private MessageReporter Reporter { get; }

    internal IPlatformHook? PlatformHook { get; }
    internal IKeyboardHook? KeyboardHook => PlatformHook as IKeyboardHook;
    internal IMouseHook? MouseHook => PlatformHook as IMouseHook;
    internal MessageLoop? MessageLoop { get; }

    public MacroListener(MessageReporter reporter)
    {
        Reporter = reporter;
        PlatformHook = GetPlatformHook();
        
        MessageLoop = GetMessageLoop(() =>
        {
            if (PlatformHook == null || !PlatformHook.MessageLoopInit())
            {
                Reporter.Error("Failed to initialize platform hook, input events will not be fired.");
            }
        });

        if (MessageLoop == null)
        {
            Reporter.Error("Failed to initialize message loop, input events will not be handled.");
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

    private IPlatformHook? GetPlatformHook()
    {
        return new OsSelector<IPlatformHook>()
            .OnWindows(() => new WindowsHook(Reporter))
            .Execute();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Dispose(bool disposing = true)
    {
        MessageLoop?.Stop();
        MessageLoop?.Dispose(disposing);
        KeyboardHook?.Dispose(disposing);
        MouseHook?.Dispose(disposing);
    }
}