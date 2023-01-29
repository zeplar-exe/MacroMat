namespace MacroMat.Input;

public readonly struct InputData
{
    private uint[] Keys { get; }
    
    public KeyInputType Type { get; }
    public ModifierKey Modifiers { get; }
    public bool IsScancode { get; }

    public IEnumerable<uint> Scancodes => IsScancode ? Keys : Enumerable.Empty<uint>();
    public IEnumerable<InputKey> InputKeys => !IsScancode ? Keys.Cast<InputKey>() : Enumerable.Empty<InputKey>();
    public int KeyCount => Keys.Length;
   

    public static InputData FromScancode(uint scancode, KeyInputType type, ModifierKey modifiers = 0)
    {
        return FromScancodes(new[] { scancode }, type, modifiers);
    }
    
    public static InputData FromScancodes(uint[] scancode, KeyInputType type, ModifierKey modifiers = 0)
    {
        return new InputData(scancode, type, modifiers, true);
    }

    public static InputData FromKey(InputKey key, KeyInputType type, ModifierKey modifiers = 0)
    {
        return FromKeys(new[] { key }, type, modifiers);
    }
    
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