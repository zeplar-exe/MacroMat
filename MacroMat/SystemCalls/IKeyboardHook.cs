using MacroMat.Input;

namespace MacroMat.SystemCalls;

internal interface IKeyboardHook : IDisposable
{
    public event KeyboardHookCallback? OnKeyEvent;

    public bool MessageLoopInit();
}

internal delegate void KeyboardHookCallback(IKeyboardHook hook, KeyboardEventArgs args);