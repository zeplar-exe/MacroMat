namespace MacroMat.SystemCalls;

internal abstract class MessageLoop : IDisposable
{
    protected Action<MessageLoop>? InitialAction { get; }

    protected MessageLoop(Action<MessageLoop>? initialAction)
    {
        InitialAction = initialAction;
    }

    public abstract void Start(Action<Exception>? exceptionCallback = null);

    public abstract void EnqueueAction(Action action);
    public abstract void Stop();

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Dispose(true);
    }

    public abstract void Dispose(bool disposing);
}