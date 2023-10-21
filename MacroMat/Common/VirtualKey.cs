using MacroMat.Input;
using Vogen;

namespace MacroMat.Common;

[ValueObject<byte>]
public partial struct VirtualKey : IKeyRepresentation
{
    public static VirtualKey Of(InputKey key)
    {
        return InputKeyTranslator.CurrentPlatformVirtual(key);
    }
    
    public static VirtualKey[] Of(params InputKey[] keys)
    {
        return keys.Select(InputKeyTranslator.CurrentPlatformVirtual).ToArray();
    }
}