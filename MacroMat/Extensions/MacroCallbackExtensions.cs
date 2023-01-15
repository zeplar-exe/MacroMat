using MacroMat.Instructions;
using MacroMat.SystemCalls;

namespace MacroMat.Extensions;

public static class MacroCallbackExtensions
{
    public static Macro OnKeyEvent(this Macro macro, Action<KeyboardEventData> action)
    {
        return OnKeyEvent(macro, _ => true, action);
    }

    public static Macro OnKeyEvent(this Macro macro, Func<KeyboardEventData, bool> predicate, Action<KeyboardEventData> action)
    {
        return macro.AddInstruction(new KeyCallbackInstruction(predicate, action));
    }
}