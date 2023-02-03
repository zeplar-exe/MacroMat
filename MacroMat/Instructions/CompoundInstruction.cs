namespace MacroMat.Instructions;

/// <summary>
/// Instruction to execute multiple <see cref="MacroInstruction"/>s sequentially in one instruction.
/// </summary>
public class CompoundInstruction : MacroInstruction
{
    private List<MacroInstruction> Instructions { get; }

    /// <inheritdoc />
    public CompoundInstruction()
    {
        Instructions = new List<MacroInstruction>();
    }

    /// <summary>
    /// Add an instruction to this compound.
    /// </summary>
    public void AddInstruction(MacroInstruction instruction)
    {
        Instructions.Add(instruction);
    }

    /// <inheritdoc />
    public override void Execute(Macro macro)
    {
        foreach (var instruction in Instructions)
        {
            instruction.Execute(macro);
        }
    }
}