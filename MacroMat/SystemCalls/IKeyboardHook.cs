using MacroMat.Input;

namespace MacroMat.SystemCalls;

internal interface IKeyboardHook : IDisposable
{
    public event KeyboardHookCallback? OnKeyEvent;
}

internal delegate void KeyboardHookCallback(IKeyboardHook hook, KeyboardEventArgs args);