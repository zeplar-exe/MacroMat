using MacroMat.Common;

namespace MacroMat.Input;

/// <summary>
/// Cross-platform representation of input.
/// </summary>
public readonly struct KeyInputData
{
    // This is supposed to be an agnostic representation of either
    // scancodes or InputKey enum values
    internal IKeyRepresentation[] Keys { get; }
    
    /// <summary>
    /// The type of input; KeyUp or KeyDown.
    /// </summary>
    public KeyInputType Type { get; }

    /// <summary>
    /// The scancodes present in this input. If <see cref="IsScancode"/> is false, an empty enumerable is returned.
    /// </summary>
    public IEnumerable<Scancode> Scancodes => Keys.OfType<Scancode>();

    /// <summary>
    /// The input keys present in this input. If <see cref="IsScancode"/> is true, an empty enumerable is returned.
    /// </summary>
    public IEnumerable<VirtualKey> VirtualKeys => Keys.OfType<VirtualKey>();
    /// <summary>
    /// The number of keys present in this input.
    /// </summary>
    public int KeyCount => Keys.Length;

    public static KeyInputData Press(params IKeyRepresentation[] keys)
    {
        return new KeyInputData(keys.ToArray(), KeyInputType.KeyDown);
    }
    
    public static KeyInputData Release(params IKeyRepresentation[] keys)
    {
        return new KeyInputData(keys.ToArray(), KeyInputType.KeyUp);
    }

    private KeyInputData(IKeyRepresentation[] keys, KeyInputType type)
    {
        Keys = keys;
        Type = type;
    }

    public bool ContainsScancode(Scancode code)
    {
        return Scancodes.Contains(code);
    }
    
    public bool ContainsVirtualKey(VirtualKey key)
    {
        return VirtualKeys.Contains(key);
    }
}