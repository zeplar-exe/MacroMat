using System.ComponentModel;

namespace MacroMat.SystemCalls.Windows;

internal class GlobalKeyboardHookEventArgs : HandledEventArgs
{
    public KeyboardState KeyboardState { get; }
    public WindowsKeyboardInputEvent KeyboardData { get; }

    public GlobalKeyboardHookEventArgs(
        WindowsKeyboardInputEvent keyboardData,
        KeyboardState keyboardState)
    {
        KeyboardData = keyboardData;
        KeyboardState = keyboardState;
    }
}