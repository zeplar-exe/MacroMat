namespace MacroMat.SystemCalls;

public abstract class MessageLoop
{
    protected Action? InitialAction { get; }
    
    public bool IsRunning { get; protected set; }

    protected MessageLoop(Action? initialAction)
    {
        InitialAction = initialAction;
    }

    public abstract void Start(Action<Exception>? exceptionCallback = null);

    public virtual void Stop()
    {
        IsRunning = false;
    }
}