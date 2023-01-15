using MacroMat.Instructions;

namespace MacroMat.Extensions;

public static class MacroWaitExtensions
{
    public static Macro Wait(this Macro macro, int milliseconds)
    {
        return macro.AddInstruction(new WaitInstruction(TimeSpan.FromMilliseconds(milliseconds)));
    }
    
    public static Macro Wait(this Macro macro, TimeSpan time)
    {
        return macro.AddInstruction(new WaitInstruction(time));
    }
}