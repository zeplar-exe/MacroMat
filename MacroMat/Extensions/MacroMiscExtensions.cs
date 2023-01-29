using MacroMat.Instructions;

namespace MacroMat.Extensions;

public static class MacroMiscExtensions
{
    public static Macro Action(this Macro macro, Action<Macro> action)
    {
        return macro.AddInstruction(new ActionInstruction(action));
    }

    public static Macro Log(this Macro macro, string message)
    {
        macro.Messages.Log(message);
        
        return macro;
    }
    
    public static Macro Error(this Macro macro, string message)
    {
        macro.Messages.Error(message);
        
        return macro;
    }
}