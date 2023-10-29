using MacroMat.Common;
using MacroMat.Input;
using MacroMat.Instructions;

namespace MacroMat.Extensions;

/// <summary>
/// Extension methods relating to input callback instructions.
/// </summary>
public static class MacroKeyCallbackExtensions
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
        new KeyCallbackInstruction(predicate, action).Execute(macro);

        return macro;
    }
    
    public static Macro OnKeyPressed(this Macro macro, IKeyRepresentation key, Action<KeyboardEventArgs> action)
    {
        return macro.OnKey(key, KeyInputType.KeyDown, action);
    }
    
    public static Macro OnKeyReleased(this Macro macro, IKeyRepresentation key, Action<KeyboardEventArgs> action)
    {
        return macro.OnKey(key, KeyInputType.KeyUp, action);
    }
    
    /// <summary>
    /// Enqueue a KeyCallbackInstruction to invoke an action whenever the specified input is
    /// caught.
    /// </summary>
    public static Macro OnKey(this Macro macro, IKeyRepresentation key, KeyInputType type, Action<KeyboardEventArgs> action)
    {
        return OnKeyEvent(macro, 
            data =>
            {
                if (data.Type != type)
                    return false;

                if (key is VirtualKey virtualKey)
                {
                    return data.VirtualCode == virtualKey;
                }
                else if (key is Scancode scancode)
                {
                    return data.HardwareScancode == scancode;
                }

                return false;
            },
            action);
    }
    
    /// <summary>
    /// Disable the specified key, that is, catching it and not allowing it to
    /// propagate.
    /// </summary>
    public static Macro DisableKey(this Macro macro, IKeyRepresentation key, KeyInputType type)
    {
        return OnKey(macro, key, type, args => args.Handled = true);
    }

    /// <summary>
    /// Remap a key to another, effectively replacing the input event. Beware of
    /// infinite recursion which can occur by mapping A to B and B to A, for example.
    /// </summary>
    public static Macro RemapKey(this Macro macro, IKeyRepresentation original, KeyInputType type, IKeyRepresentation replace)
    {
        var instruction = new SimulateKeyboardInstruction(KeyInputData.Press(replace));
        
        return OnKey(macro, original, type,
            args => 
            {
                args.Handled = true;
                instruction.Execute(macro);
            });
    }
}