using MacroMat.Input;

namespace MacroMat.Instructions;

/// <summary>
/// Instruction to initialize a callback with a predicate for key input events.
/// </summary>
public class KeyCallbackInstruction : MacroInstruction
{
    private Func<KeyboardEventData, bool> Predicate { get; }
    private Action<KeyboardEventArgs> Action { get; }

    /// <inheritdoc />
    public KeyCallbackInstruction(Func<KeyboardEventData, bool> predicate, Action<KeyboardEventArgs> action)
    {
        Predicate = predicate;
        Action = action;
    }

    /// <inheritdoc />
    public override void Execute(Macro macro)
    {
        if (macro.Listener.KeyboardHook == null)
            return;
        
        macro.Listener.KeyboardHook.OnKeyEvent += (hook, args) =>
        {
            if (!Predicate.Invoke(args.Data))
                return;
            
            Action.Invoke(args);
        };
    }
}