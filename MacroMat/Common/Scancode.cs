using MacroMat.Input;
using Vogen;

namespace MacroMat.Common;

/// <summary>
/// <see cref="IKeyRepresentation"/> for hardware-dependent physical key locations.
/// </summary>
[ValueObject<ushort>]
public partial struct Scancode : IKeyRepresentation
{
    /// <summary>
    /// Create a <see cref="Scancode"/> from the given <see cref="InputKey"/>.
    /// </summary>
    /// <param name="key">Key to create a <see cref="Scancode"/> from.</param>
    /// <returns>
    /// <see cref="Scancode"/> which corresponds to the physical location of
    /// the <see cref="InputKey"/> on the keyboard.
    /// </returns>
    public static Scancode Of(InputKey key)
    {
        return InputKeyTranslator.CurrentPlatformScancode(key);
    }
    
    /// <summary>
    /// Create n <see cref="Scancode">Scancodes</see> from n  <see cref="InputKey">InputKeys</see>.
    /// </summary>
    /// <param name="keys">Keys to create <see cref="Scancode">Scancodes</see> from.</param>
    /// <returns>
    /// <see cref="Scancode">Scancodes</see> which correspond to the given
    /// <see cref="InputKey">InputKeys</see>.
    /// </returns>
    public static IEnumerable<IKeyRepresentation> Of(params InputKey[] keys)
    {
        return keys.Select(InputKeyTranslator.CurrentPlatformScancode).Cast<IKeyRepresentation>().ToArray();
    }
}