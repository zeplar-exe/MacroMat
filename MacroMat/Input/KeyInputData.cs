using MacroMat.Common;

namespace MacroMat.Input;

/// <summary>
/// Cross-platform representation of input.
/// </summary>
public readonly struct KeyInputData
{
    public IReadOnlyCollection<IKeyRepresentation> Keys { get; }
    
    /// <summary>
    /// The type of input; KeyUp or KeyDown.
    /// </summary>
    public KeyInputType Type { get; }

    /// <summary>
    /// The scancodes present in this input.
    /// </summary>
    public IEnumerable<Scancode> Scancodes => Keys.OfType<Scancode>();

    /// <summary>
    /// The input keys present in this input.
    /// </summary>
    public IEnumerable<VirtualKey> VirtualKeys => Keys.OfType<VirtualKey>();
    /// <summary>
    /// The number of keys present in this input.
    /// </summary>
    public int KeyCount => Keys.Count;

    public static KeyInputData Press(params IKeyRepresentation[] keys)
    {
        return new KeyInputData(keys.ToArray().AsReadOnly(), KeyInputType.KeyDown);
    }
    
    public static KeyInputData Press(IEnumerable<IKeyRepresentation> keys)
    {
        return new KeyInputData(keys.ToArray(), KeyInputType.KeyDown);
    }
    
    public static KeyInputData Release(params IKeyRepresentation[] keys)
    {
        return new KeyInputData(keys.ToArray(), KeyInputType.KeyUp);
    }
    
    public static KeyInputData Release(IEnumerable<IKeyRepresentation> keys)
    {
        return new KeyInputData(keys.ToArray(), KeyInputType.KeyUp);
    }

    private KeyInputData(IReadOnlyCollection<IKeyRepresentation> keys, KeyInputType type)
    {
        Keys = keys;
        Type = type;
    }

    public bool ContainsKey(IKeyRepresentation key)
    {
        return Keys.Contains(key);
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