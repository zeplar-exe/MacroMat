using System.Text;

using MacroMat.Input;
using MacroMat.Instructions;

namespace MacroMat.Extensions;

public static class MacroSimulateExtensions
{
    public static Macro SimulateScancode(this Macro macro, uint scancode, KeyInputType type, ModifierKey modifiers = 0)
    {
        return macro.AddInstruction(new SimulateKeyInstruction(scancode, type, modifiers));
    }

    public static Macro SimulateScancodes(this Macro macro, uint[] scancode, KeyInputType type, ModifierKey modifiers = 0)
    {
        return macro.AddInstruction(new SimulateKeyInstruction(scancode, type, modifiers));
    }
    
    public static Macro SimulateKey(this Macro macro, InputKey key, KeyInputType type, ModifierKey modifiers = 0)
    {
        return macro.AddInstruction(new SimulateKeyInstruction(key, type, modifiers));
    }
    
    public static Macro SimulateKeys(this Macro macro, InputKey[] key, KeyInputType type, ModifierKey modifiers = 0)
    {
        return macro.AddInstruction(new SimulateKeyInstruction(key, type, modifiers));
    }
    
    public static Macro SimulateUnicode(this Macro macro, string s)
    {
        var compound = new CompoundInstruction();
        
        compound.AddInstruction(new SendUnicodeInstructions(s, KeyInputType.KeyDown));
        compound.AddInstruction(new SendUnicodeInstructions(s, KeyInputType.KeyUp));
        
        return macro.AddInstruction(compound);
    }
}