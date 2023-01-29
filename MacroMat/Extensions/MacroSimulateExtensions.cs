using System.Text;

using MacroMat.Input;
using MacroMat.Instructions;

namespace MacroMat.Extensions;

public static class MacroSimulateExtensions
{
    public static Macro SimulateInput(this Macro macro, InputData data)
    {
        return macro.AddInstruction(new SimulateKeyInstruction(data));
    }
    
    public static Macro SimulateUnicode(this Macro macro, string s)
    {
        var compound = new CompoundInstruction();
        
        compound.AddInstruction(new SendUnicodeInstruction(s, KeyInputType.KeyDown));
        compound.AddInstruction(new SendUnicodeInstruction(s, KeyInputType.KeyUp));
        
        return macro.AddInstruction(compound);
    }
}