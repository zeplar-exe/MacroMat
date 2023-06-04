﻿using MacroMat.Common;

namespace MacroMat.Input;

/// <summary>
/// Cross-platform representation of input.
/// </summary>
public readonly struct InputData
{
    private Scancode[] Keys { get; }
    
    /// <summary>
    /// The type of input; KeyUp or KeyDown.
    /// </summary>
    public KeyInputType Type { get; }
    /// <summary>
    /// Whether this input contains scancodes or <see cref="InputKey"/>s.
    /// </summary>
    public bool IsScancode { get; }

    /// <summary>
    /// The scancodes present in this input. If <see cref="IsScancode"/> is false, an empty enumerable is returned.
    /// </summary>
    public IEnumerable<Scancode> Scancodes => IsScancode ? Keys : Enumerable.Empty<Scancode>();
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
    public static InputData FromScancode(Scancode scancode, KeyInputType type)
    {
        return FromScancodes(new[] { scancode }, type);
    }
    
    /// <summary>
    /// Create an <see cref="InputData"/> from multiple scancodes with its type and modifier keys.
    /// </summary>
    public static InputData FromScancodes(Scancode[] scancode, KeyInputType type)
    {
        return new InputData(scancode, type, true);
    }

    /// <summary>
    /// Create an <see cref="InputData"/> from a single <see cref="InputKey"/> with its type and modifier keys.
    /// </summary>
    public static InputData FromKey(InputKey key, KeyInputType type)
    {
        return FromKeys(new[] { key }, type);
    }
    
    /// <summary>
    /// Create an <see cref="InputData"/> from multiple <see cref="InputKey"/>s with its type and modifier keys.
    /// </summary>
    public static InputData FromKeys(InputKey[] key, KeyInputType type)
    {
        // InputKeyTranslator.ToWindowsScancode()
        
        return new InputData(key.Cast<Scancode>().ToArray(), type, false);
    }

    private InputData(Scancode[] keys, KeyInputType type, bool isScancode)
    {
        Keys = keys;
        Type = type;
        IsScancode = isScancode;
    }

    public bool ContainsKey(InputKey key)
    {
        return InputKeys.Contains(key);
    }

    public bool ContainsScancode(Scancode code)
    {
        return Scancodes.Contains(code);
    }
}