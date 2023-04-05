using MacroMat.Input;
using MacroMat.Instructions;

namespace MacroMat.Extensions;

/// <summary>
/// Extension methods relating to input callback instructions.
/// </summary>
public static class MacroCallbackExtensions
{
    /// <summary>
    /// Enqueue a KeyCallbackInstruction to invoke an action whenever any key event is caught.
    /// </summary>
    public static Macro OnKeyEvent(this Macro macro, Action<KeyboardEventArgs> action)
    {
        return OnKeyEvent(macro, _ => true, action);
    }

    /// <summary>
    /// Enqueue a KeyCallbackInstruction to invoke an action whenever any key event is caught,
    /// based on a predicate.
    /// </summary>
    public static Macro OnKeyEvent(this Macro macro, Func<KeyboardEventData, bool> predicate, Action<KeyboardEventArgs> action)
    {
        return macro.AddInstruction(new KeyCallbackInstruction(predicate, action));
    }

    /// <summary>
    /// Enqueue a KeyCallbackInstruction to invoke an action whenever the specified input is
    /// caught.
    /// </summary>
    public static Macro OnKey(this Macro macro, InputData key, Action<KeyboardEventArgs> action)
    {
        return macro.OnKeyEvent(
            data =>
            {
                if (key.KeyCount != 1)
                    return false;

                if (key.IsScancode)
                {
                    // TODO: check against multiple keys?
                    if (key.Scancodes.First() != data.HardwareScancode)
                        return false;
                }
                else
                {
                    var firstKey = key.InputKeys.FirstOrDefault();

                    if (firstKey != InputKey.None)
                    {
                        var virtualCode = InputKeyTranslator.CurrentPlatformVirtual(firstKey);

                        if (virtualCode == null)
                            return false;

                        if (virtualCode != data.VirtualCode)
                            return false;
                    }
                }

                if (key.Type != data.Type)
                    return false;

                if (key.ContainsKey(InputKey.Alt) && !data.IsAlt)
                    return false;

                // TODO: What about other modifiers?

                return true;
            },
            action);
    }
    
    /// <summary>
    /// Disable the specified key, that is, catching it and not allowing it to
    /// propagate.
    /// </summary>
    public static Macro DisableKey(this Macro macro, InputData key)
    {
        return macro.OnKey(key, args => args.Handled = true);
    }

    /// <summary>
    /// Remap a key to another, effectively replacing the input event. Beware of
    /// infinite recursion which can occur by mapping A to B and B to A, for example.
    /// </summary>
    public static Macro RemapKey(this Macro macro, InputData original, InputData replace)
    {
        var instruction = new SimulateKeyInstruction(replace);
        
        return macro.OnKey(original, 
            args => 
            {
                args.Handled = true;
                instruction.Execute(macro);
            });
    }
}