namespace MacroMat.Input;

/// <summary>
/// Cross-platform representation of input.
/// </summary>
public readonly struct InputData
{
    private uint[] Keys { get; }
    
    /// <summary>
    /// The type of input; KeyUp or KeyDown.
    /// </summary>
    public KeyInputType Type { get; }
    /// <summary>
    /// Modifier keys attached to the input.
    /// </summary>
    public ModifierKey Modifiers { get; }
    /// <summary>
    /// Whether this input contains scancodes or <see cref="InputKey"/>s.
    /// </summary>
    public bool IsScancode { get; }

    /// <summary>
    /// The scancodes present in this input. If <see cref="IsScancode"/> is false, an empty enumerable is returned.
    /// </summary>
    public IEnumerable<uint> Scancodes => IsScancode ? Keys : Enumerable.Empty<uint>();
    /// <summary>
    /// The input keys present in this input. If <see cref="IsScancode"/> is true, an empty enumerable is returned.
    /// </summary>
    public IEnumerable<InputKey> InputKeys => !IsScancode ? Keys.Cast<InputKey>() : Enumerable.Empty<InputKey>();
    /// <summary>
    /// The number of keys present in this input.
    /// </summary>
    public int KeyCount => Keys.Length;
   
    /// <summary>
    /// Create an <see cref="InputData"/> from a single scancode with its type and modifier keys.
    /// </summary>
    public static InputData FromScancode(uint scancode, KeyInputType type, ModifierKey modifiers = 0)
    {
        return FromScancodes(new[] { scancode }, type, modifiers);
    }
    
    /// <summary>
    /// Create an <see cref="InputData"/> from multiple scancodes with its type and modifier keys.
    /// </summary>
    public static InputData FromScancodes(uint[] scancode, KeyInputType type, ModifierKey modifiers = 0)
    {
        return new InputData(scancode, type, modifiers, true);
    }

    /// <summary>
    /// Create an <see cref="InputData"/> from a single <see cref="InputKey"/> with its type and modifier keys.
    /// </summary>
    public static InputData FromKey(InputKey key, KeyInputType type, ModifierKey modifiers = 0)
    {
        return FromKeys(new[] { key }, type, modifiers);
    }
    
    /// <summary>
    /// Create an <see cref="InputData"/> from multiple <see cref="InputKey"/>s with its type and modifier keys.
    /// </summary>
    public static InputData FromKeys(InputKey[] key, KeyInputType type, ModifierKey modifiers = 0)
    {
        return new InputData(key.Cast<uint>().ToArray(), type, modifiers, false);
    }

    private InputData(uint[] keys, KeyInputType type, ModifierKey modifiers, bool isScancode)
    {
        Keys = keys;
        Type = type;
        Modifiers = modifiers;
        IsScancode = isScancode;
    }
}