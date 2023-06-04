﻿using MacroMat.Common;
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
        return macro.AddInstruction(new KeyCallbackInstruction(predicate, action));
    }
    
    /// <summary>
    /// Enqueue a KeyCallbackInstruction to invoke an action whenever the specified input is
    /// caught.
    /// </summary>
    public static Macro OnKey(this Macro macro, InputKey key, KeyInputType type, Action<KeyboardEventArgs> action)
    {
        return OnKeyInternal(macro, key, type, true, action);
    }
    
    public static Macro OnKeyAlternate(this Macro macro, InputKey key, KeyInputType type, Action<KeyboardEventArgs> action)
    {
        return OnKeyInternal(macro, key, type, false, action);
    }
    
    private static Macro OnKeyInternal(this Macro macro, InputKey key, KeyInputType type, bool alternate, 
        Action<KeyboardEventArgs> action)
    {
        return OnKeyEvent(macro, 
            data =>
            {
                if (key != InputKey.None)
                {
                    var virtualCode = InputKeyTranslator.CurrentPlatformVirtual(key);

                    if (virtualCode == null)
                        return false;

                    if (virtualCode.Value != data.VirtualCode)
                        return false;
                }

                if (type != data.Type)
                    return false;

                if (alternate && !data.IsAlt)
                    return false;

                return true;
            },
            action);
    }
    
    public static Macro OnInput(this Macro macro, InputData data, Action<KeyboardEventArgs> action)
    {
        return macro.Action(actionMacro =>
        {
            var os = new OsSelector();
            
            os.OnWindows(() =>
                {
                    foreach (var key in data.InputKeys)
                    { // data.IsScancode
                        InputKeyTranslator.ToWindowsVirtual(key);
                    }
                })
                .Execute();
        });
    }
    
    /// <summary>
    /// Disable the specified key, that is, catching it and not allowing it to
    /// propagate.
    /// </summary>
    public static Macro DisableKey(this Macro macro, InputKey key, KeyInputType type)
    {
        return OnKey(macro, key, type, args => args.Handled = true);
    }

    /// <summary>
    /// Remap a key to another, effectively replacing the input event. Beware of
    /// infinite recursion which can occur by mapping A to B and B to A, for example.
    /// </summary>
    public static Macro RemapKey(this Macro macro, InputKey original, KeyInputType type, InputKey replace)
    {
        var instruction = new SimulateKeyboardInstruction(InputData.FromKey(replace, type));
        
        return OnKey(macro, original, type,
            args => 
            {
                args.Handled = true;
                instruction.Execute(macro);
            });
    }
}