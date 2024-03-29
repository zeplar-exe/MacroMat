﻿using MacroMat.Input;

namespace MacroMat.SystemCalls;

public interface IKeyboardHook : IDisposable
{
    public event KeyboardHookCallback? OnKeyEvent;

    public void MessageLoopInit();
}

public delegate void KeyboardHookCallback(IKeyboardHook hook, KeyboardEventData data);