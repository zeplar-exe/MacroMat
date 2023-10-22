using MacroMat.Input;
using Vogen;

namespace MacroMat.Common;

[ValueObject<ushort>]
public partial struct Scancode : IKeyRepresentation
{
    public static Scancode Of(InputKey key)
    {
        return InputKeyTranslator.CurrentPlatformScancode(key);
    }
    
    public static IEnumerable<IKeyRepresentation> Of(params InputKey[] keys)
    {
        return keys.Select(InputKeyTranslator.CurrentPlatformScancode).Cast<IKeyRepresentation>().ToArray();
    }
}