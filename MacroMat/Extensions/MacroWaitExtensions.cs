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
        return macro.AddInstruction(new WaitInstruction(TimeSpan.FromMilliseconds(milliseconds)));
    }
    
    /// <summary>
    /// Enqueue a WaitInstruction to wait for the specified amount of time.
    /// </summary>
    public static Macro Wait(this Macro macro, TimeSpan time)
    {
        return macro.AddInstruction(new WaitInstruction(time));
    }
}