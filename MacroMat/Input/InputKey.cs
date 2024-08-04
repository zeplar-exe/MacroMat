namespace MacroMat.Input;

/// <summary>
/// Cross-platform representation of keyboard keys.
/// </summary>
public enum InputKey : short
{
    // Guidelines for adding new input keys:
    // 1. Is it a commonly used key, whether physically or automatically?
    // 2. If it's not commonly used, is it present on at least 2 platforms?
    
    F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14,
    F15, F16, F17, F18, F19, F20, F21, F22, F23, F24,
    
    VolumeMute, VolumeDown, VolumeUp, PlaybackPrevious, PlaybackPlayPause,
    PlaybackNext,
    
    Backtick,
    Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine,
    Dash, Underscore, Equals, Plus,
    LeftBracket, RightBracket,
    VerticalBar, Backslash, Semicolon, Colon, SingleQuote,
    Comma, Period, Slash,

    A, B, C, D, E, F, G, H, I, 
    J, K, L, M, N, O, P, Q, R, 
    S, T, U, V, W, X, Y, Z,

    Shift,
    LeftShift,
    RightShift,
    
    Alt,
    LeftAlt,
    RightAlt,
    
    Control,
    LeftControl,
    RightControl,
    WindowsKey,
    LeftWindowsKey,
    RightWindowsKey,
    
    Space,
    
    Enter, Tab, CapsLock, Numlock, Escape, Delete, Backspace,
    
    Num0, Num1, Num2, Num3, Num4, Num5, Num6, Num7, Num8, Num9,
    
    LeftArrow, RightArrow, UpArrow, DownArrow,
    
    Home, End, PageUp, PageDown, Insert, PrintScreen
}