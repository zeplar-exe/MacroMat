using System.ComponentModel;

namespace MacroMat.SystemCalls.Windows;

internal class GlobalKeyboardHookEventArgs : HandledEventArgs
{
    public WindowsKeyboardState KeyboardState { get; }
    public WindowsKeyboardInputEvent KeyboardData { get; }

    public GlobalKeyboardHookEventArgs(
        WindowsKeyboardInputEvent keyboardData,
        WindowsKeyboardState keyboardState)
    {
        KeyboardData = keyboardData;
        KeyboardState = keyboardState;
    }
}