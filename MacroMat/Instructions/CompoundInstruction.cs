namespace MacroMat.Instructions;

public class CompoundInstruction : MacroInstruction
{
    private List<MacroInstruction> Instructions { get; }

    public CompoundInstruction()
    {
        Instructions = new List<MacroInstruction>();
    }

    public void AddInstruction(MacroInstruction instruction)
    {
        Instructions.Add(instruction);
    }

    public override void Execute(Macro macro)
    {
        foreach (var instruction in Instructions)
        {
            instruction.Execute(macro);
        }
    }
}