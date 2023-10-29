namespace MacroMat.Input;

/// <summary>
/// Cross-platform representation of keyboard keys.
/// </summary>
public enum InputKey : short
{
    F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14,
    F15, F16, F17, F18, F19, F20, F21, F22, F23, F24,
    
    VolumeMute, VolumeDown, VolumeUp, PlaybackPrevious, PlaybackPlayPause,
    PlaybackNext,
    
    Backtick, Tilde,
    Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine,
    ExclamationPoint, At, Hashtag, Dollar, Percent, Caret, Ampersand, Star,
    LeftParenthesis, RightParenthesis, Dash, Underscore, Equals, Plus,
    LeftBracket, LeftCurlyBracket, RightBracket, RightCurlyBracket,
    VerticalBar, Backslash, Semicolon, Colon, SingleQuote, DoubleQuote,
    Comma, LessThan, Period, GreaterThan, Slash, QuestionMark,

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
    
    Enter, Tab, CapsLock, Numlock, Escape, Delete, Backspace,
    
    Num0, Num1, Num2, Num3, Num4, Num5, Num6, Num7, Num8, Num9,
    
    LeftArrow, RightArrow, UpArrow, DownArrow
}