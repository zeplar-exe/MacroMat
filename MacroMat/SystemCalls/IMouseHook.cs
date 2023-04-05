using MacroMat.Input;

namespace MacroMat.SystemCalls;

internal interface IMouseHook
{
    public event MouseHookCallback? OnMouseEvent;
}

internal delegate void MouseHookCallback(IMouseHook hook, MouseEventArgs args);