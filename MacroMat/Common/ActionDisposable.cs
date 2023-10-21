namespace MacroMat.Common;

internal class ActionDisposable : IDisposable
{
    private Action Action { get; }

    public ActionDisposable(Action action)
    {
        Action = action;
    }

    public void Dispose()
    {
        Action.Invoke();
    }
}