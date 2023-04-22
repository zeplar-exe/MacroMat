namespace MacroMat.SystemCalls.Windows;

internal enum WindowsKeyboardState : int
{
    KeyDown = 0x0100,
    KeyUp = 0x0101,
    SysKeyDown = 0x0104,
    SysKeyUp = 0x0105
}