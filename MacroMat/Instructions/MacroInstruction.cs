namespace MacroMat.Instructions;

/// <summary>
/// Base class for instructions run in a <see cref="Macro"/>.
/// </summary>
public abstract class MacroInstruction
{
    /// <summary>
    /// Execute this MacroInstruction with the specified macro as context.
    /// </summary>
    public abstract void Execute(Macro macro);
}