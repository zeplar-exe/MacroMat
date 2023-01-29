namespace MacroMat.Instructions;

public class ActionInstruction : MacroInstruction
{
    private Action<Macro> Action { get; }

    public ActionInstruction(Action<Macro> action)
    {
        Action = action;
    }

    public override void Execute(Macro macro)
    {
        Action.Invoke(macro);
    }
}