using MacroMat.Input;
using Vogen;

namespace MacroMat.Common;

/// <summary>
/// <see cref="IKeyRepresentation"/> for OS-dependent virtual keys.
/// </summary>
[ValueObject<short>]
public partial struct VirtualKey : IKeyRepresentation
{
    /// <summary>
    /// Create a <see cref="VirtualKey"/> from the given <see cref="InputKey"/>.
    /// </summary>
    /// <param name="key">Key to create a <see cref="VirtualKey"/> from.</param>
    /// <returns>
    /// <see cref="VirtualKey"/> which corresponds to the given <see cref="InputKey"/>.
    /// </returns>
    public static VirtualKey Of(InputKey key)
    {
        return InputKeyTranslator.CurrentPlatformVirtual(key);
    }
    
    /// <summary>
    /// Create n <see cref="VirtualKey">VirtualKeys</see> from n  <see cref="InputKey">InputKeys</see>.
    /// </summary>
    /// <param name="keys">Keys to create <see cref="VirtualKey">VirtualKeys</see> from.</param>
    /// <returns>
    /// <see cref="Scancode">VirtualKeys</see> which correspond to the given
    /// <see cref="InputKey">InputKeys</see>.
    /// </returns>
    public static IEnumerable<IKeyRepresentation> Of(params InputKey[] keys)
    {
        return keys.Select(InputKeyTranslator.CurrentPlatformVirtual).Cast<IKeyRepresentation>().ToArray();
    }

    public static implicit operator VirtualKey(InputKey key) => Of(key);
}