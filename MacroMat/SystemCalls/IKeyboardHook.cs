using MacroMat.Input;

namespace MacroMat.SystemCalls;

/// <summary>
/// Interface for keyboard hooks.
/// </summary>
internal interface IKeyboardHook : IDisposable
{
    /// <summary>
    /// Keyboard event.
    /// </summary>
    public event KeyboardHookCallback? OnKeyEvent;
    public void Dispose(bool disposing = true);
}

/// <summary>
/// Keyboard event callback delegate.
/// </summary>
internal delegate void KeyboardHookCallback(IKeyboardHook hook, KeyboardEventArgs args);