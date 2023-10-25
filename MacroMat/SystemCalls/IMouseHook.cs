using MacroMat.Input;

namespace MacroMat.SystemCalls;

/// <summary>
/// Interface for mouse hooks.
/// </summary>
internal interface IMouseHook
{
    /// <summary>
    /// Mouse event.
    /// </summary>
    public event MouseHookCallback? OnMouseEvent;
}

/// <summary>
/// Mouse event callback delegate.
/// </summary>
internal delegate void MouseHookCallback(IMouseHook hook, MouseEventArgs args);