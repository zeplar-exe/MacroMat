using MacroMat.Input;

namespace MacroMat.SystemCalls;

/// <summary>
/// Interface for mouse hooks.
/// </summary>
internal interface IMouseHook : IDisposable
{
    /// <summary>
    /// Mouse event.
    /// </summary>
    public event MouseHookCallback? OnMouseEvent;
    public void Dispose(bool disposing = true);
}

/// <summary>
/// Mouse event callback delegate.
/// </summary>
internal delegate void MouseHookCallback(IMouseHook hook, MouseEventArgs args);