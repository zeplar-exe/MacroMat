namespace MacroMat.Instructions;

/// <summary>
/// Instruction to invoke an <see cref="System.Action"/>.
/// </summary>
public class ActionInstruction : MacroInstruction
{
    private Action<Macro> Action { get; }

    /// <inheritdoc />
    public ActionInstruction(Action<Macro> action)
    {
        Action = action;
    }

    /// <inheritdoc />
    public override void Execute(Macro macro)
    {
        Action.Invoke(macro);
    }
}