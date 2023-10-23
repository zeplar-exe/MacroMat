using MacroMat.Input;
using Vogen;

namespace MacroMat.Common;

[ValueObject<short>]
public partial struct VirtualKey : IKeyRepresentation
{
    public static VirtualKey Of(InputKey key)
    {
        return InputKeyTranslator.CurrentPlatformVirtual(key);
    }
    
    public static IEnumerable<IKeyRepresentation> Of(params InputKey[] keys)
    {
        return keys.Select(InputKeyTranslator.CurrentPlatformVirtual).Cast<IKeyRepresentation>().ToArray();
    }
}