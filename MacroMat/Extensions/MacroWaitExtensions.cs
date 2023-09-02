using MacroMat.Instructions;

namespace MacroMat.Extensions;

/// <summary>
/// Extension methods relating to macro timeouts and delays.
/// </summary>
public static class MacroWaitExtensions
{
    /// <summary>
    /// Enqueue a WaitInstruction to wait for the specified amount of milliseconds.
    /// </summary>
    public static Macro Wait(this Macro macro, int milliseconds)
    {
        new WaitInstruction(TimeSpan.FromMilliseconds(milliseconds)).Execute(macro);
        
        return macro;
    }
    
    /// <summary>
    /// Enqueue a WaitInstruction to wait for the specified amount of time.
    /// </summary>
    public static Macro Wait(this Macro macro, TimeSpan time)
    {
        new WaitInstruction(time).Execute(macro);
        
        return macro;
    }
}