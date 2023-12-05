﻿namespace MacroMat.SystemCalls;

internal abstract class MessageLoop : IDisposable
{
    protected Action? InitialAction { get; }
    protected Queue<Action> RequestedActions { get; }

    public bool IsRunning { get; protected set; }

    protected MessageLoop(Action? initialAction)
    {
        RequestedActions = new Queue<Action>();
        InitialAction = initialAction;
    }

    public abstract void Start(Action<Exception>? exceptionCallback = null);

    public virtual void EnqueueAction(Action action)
    {
        RequestedActions.Enqueue(action);
    }
    
    public virtual void Stop()
    {
        IsRunning = false;
    }

    public virtual void Dispose()
    {
        
    }

    public virtual void Dispose(bool disposing)
    {
        
    }
}