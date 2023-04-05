namespace MacroMat.Input;

/// <summary>
/// Cross-platform representation of keyboard keys.
/// </summary>
public enum InputKey : uint
{
    None = 0,
    
    Zero, One, Two, 
    Three, Four, Five, 
    Six, Seven, Eight, 
    Nine,
    
    A, B, C, 
    D, E, F, 
    G, H, I, 
    J, K, L, 
    M, N, O, 
    P, Q, R, 
    S, T, U, 
    V, W, X, 
    Y, Z,
    
    Shift,
    LeftShift,
    RightShift,
    
    Alt,
    LeftAlt,
    RightAlt,
    
    Control,
    LeftControl,
    RightControl,
}