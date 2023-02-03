namespace MacroMat.Input;

/// <summary>
/// Modifier keys for <see cref="InputData"/>.
/// </summary>
[Flags]
public enum ModifierKey
{
    None = 0,
    
    Shift = 1<<1,
    LeftShift = 1<<2,
    RightShift = 1<<3,
    
    Alt = 1<<4,
    LeftAlt = 1<<5,
    RightAlt = 1<<6,
    
    Control = 1<<7,
    LeftControl = 1<<8,
    RightControl = 1<<9,
}