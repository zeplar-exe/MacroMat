﻿using MacroMat.Input;
using MacroMat.Instructions;

namespace MacroMat.Extensions;

public static class MacroMouseCallbackExtensions
{
    public static Macro OnMouseEvent(this Macro macro, Action<MouseEventArgs> action)
    {
        return OnMouseEvent(macro, _ => true, action);
    }
    
    public static Macro OnMouseEvent(this Macro macro, Func<MouseEventData, bool> predicate, Action<MouseEventArgs> action)
    {
        return macro.EnqueueInstruction(new MouseCallbackInstruction(predicate, action));
    }
}