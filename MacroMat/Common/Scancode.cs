using MacroMat.Input;
using Vogen;

namespace MacroMat.Common;

[ValueObject<short>]
public partial struct Scancode : IKeyRepresentation
{
    public static Scancode Of(InputKey key)
    {
        return InputKeyTranslator.CurrentPlatformScancode(key);
    }
    
    public static Scancode[] Of(params InputKey[] keys)
    {
        return keys.Select(InputKeyTranslator.CurrentPlatformScancode).ToArray();
    }
}