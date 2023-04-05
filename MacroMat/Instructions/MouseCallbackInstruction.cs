using MacroMat.Input;

namespace MacroMat.Instructions;

public class MouseCallbackInstruction : MacroInstruction
{
    private Func<MouseEventData, bool> Predicate { get; }
    private Action<MouseEventArgs> Action { get; }
    
    public MouseCallbackInstruction(Func<MouseEventData, bool> predicate, Action<MouseEventArgs> action)
    {
        Predicate = predicate;
        Action = action;
    }

    /// <inheritdoc />
    public override void Execute(Macro macro)
    {
        if (macro.Listener.MouseHook == null)
            return;
        
        macro.Listener.MouseHook.OnMouseEvent += (hook, args) =>
        {
            if (!Predicate.Invoke(args.Data))
                return;
            
            Action.Invoke(args);
        };
    }
}