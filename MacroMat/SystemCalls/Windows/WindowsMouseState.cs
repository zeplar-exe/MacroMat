﻿namespace MacroMat.SystemCalls.Windows;

public enum WindowsMouseState : uint
{
    LButtonDown = 0x0201,
    LButtonUp = 0x202,
    MouseMove = 0x200,
    MouseWheel = 0x20A,
    RButtonDown = 0x204,
    RButtonUp = 0x205
}