using MacroMat.Instructions;

namespace MacroMat.Extensions;

/// <summary>
/// Miscellaneous macro instruction extensions.
/// </summary>
public static class MacroMiscExtensions
{
    /// <summary>
    /// Enqueue an ActionInstruction to invoke the specified action.
    /// </summary>
    public static Macro Action(this Macro macro, Action<Macro> action)
    {
        return macro.EnqueueInstruction(new ActionInstruction(action));
    }

    /// <summary>
    /// Enqueue an ActionInstruction to log the specified message.
    /// </summary>
    public static Macro Log(this Macro macro, string message)
    {
        return macro.Action(m => m.Messages.Log(message));
    }
    
    /// <summary>
    /// Enqueue an ActionInstruction to log the specified error.
    /// </summary>
    public static Macro Error(this Macro macro, string message)
    {
        return macro.Action(m => m.Messages.Error(message));
    }
}