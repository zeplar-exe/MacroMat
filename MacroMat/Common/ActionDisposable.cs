namespace MacroMat.Common;

/// <summary>
/// IDisposable implementation which invokes an <see cref="System.Action"/> on disposal.
/// </summary>
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