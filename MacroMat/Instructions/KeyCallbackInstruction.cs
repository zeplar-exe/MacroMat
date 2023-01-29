using MacroMat.SystemCalls;

namespace MacroMat.Instructions;

public class KeyCallbackInstruction : MacroInstruction
{
    private Func<KeyboardEventData, bool> Predicate { get; }
    private Action<KeyboardEventArgs> Action { get; }

    public KeyCallbackInstruction(Func<KeyboardEventData, bool> predicate, Action<KeyboardEventArgs> action)
    {
        Predicate = predicate;
        Action = action;
    }

    public override void Execute(Macro macro)
    {
        macro.Listener.AddCallback((hook, args) =>
        {
            if (!Predicate.Invoke(args.Data))
                return;
            
            Action.Invoke(args);
        });
    }
}