using MacroMat.Input;
using MacroMat.Instructions;

namespace MacroMat.Extensions;

/// <summary>
/// Extension methods relating to simulated input instruction.
/// </summary>
public static class MacroSimulateExtensions
{
    /// <summary>
    /// Enqueue a SimulateKeyboardInstruction to simulate the specified input.
    /// </summary>
    /// <param name="macro"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static Macro SimulateInput(this Macro macro, KeyInputData data)
    {
        new SimulateKeyboardInstruction(data).Execute(macro);
        
        return macro;
    }
    
    /// <summary>
    /// Enqueues SendUnicodeInstructions to write the specified unicode string once.
    /// </summary>
    /// <remarks>
    /// This method creates two SendUnicodeInstructions for KeyDown and KeyUp
    /// using a compound instruction.
    /// </remarks>
    public static Macro SimulateUnicode(this Macro macro, string s)
    {
        var compound = new CompoundInstruction();
        
        compound.AddInstruction(new SendUnicodeInstruction(s, KeyInputType.KeyDown));
        compound.AddInstruction(new SendUnicodeInstruction(s, KeyInputType.KeyUp));
        
        compound.Execute(macro);
        
        return macro;
    }
}