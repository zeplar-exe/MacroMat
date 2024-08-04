namespace MacroMat.SystemCalls.Windows;

internal enum WindowsMouseState : uint
{
    LButtonDown = 0x0201,
    LButtonUp = 0x202,
    MouseMove = 0x200,
    MouseWheel = 0x20A,
    MouseHWheel = 0x20E,
    RButtonDown = 0x204,
    RButtonUp = 0x205
}