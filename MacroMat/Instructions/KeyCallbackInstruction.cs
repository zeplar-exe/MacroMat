using MacroMat.SystemCalls;

namespace MacroMat.Instructions;

public class KeyCallbackInstruction : MacroInstruction
{
    private Func<KeyboardEventData, bool> Predicate { get; }
    private Action<KeyboardEventData> Action { get; }

    public KeyCallbackInstruction(Func<KeyboardEventData, bool> predicate, Action<KeyboardEventData> action)
    {
        Predicate = predicate;
        Action = action;
    }

    public override void Execute(Macro macro)
    {
        macro.Listener.AddCallback((hook, data) =>
        {
            if (!Predicate.Invoke(data))
                return;
            
            Action.Invoke(data);
        });
    }
}