using MacroMat.Input;
using MacroMat.Instructions;
using MacroMat.SystemCalls;

namespace MacroMat.Extensions;

public static class MacroCallbackExtensions
{
    public static Macro OnKeyEvent(this Macro macro, Action<KeyboardEventArgs> action)
    {
        return OnKeyEvent(macro, _ => true, action);
    }

    public static Macro OnKeyEvent(this Macro macro, Func<KeyboardEventData, bool> predicate, Action<KeyboardEventArgs> action)
    {
        return macro.AddInstruction(new KeyCallbackInstruction(predicate, action));
    }

    public static Macro RemapKey(this Macro macro, InputData original, InputData replace)
    {
        var instruction = new SimulateKeyInstruction(replace);
        
        return macro.OnKeyEvent(
            data =>
            {
                if (original.KeyCount != 1)
                    return false;
                
                if (original.IsScancode)
                {
                    if (original.Scancodes.First() != data.HardwareScancode)
                        return false;
                }
                else
                {
                    var virtualCode = InputKeyTranslator.CurrentPlatformVirtual(original.InputKeys.First());

                    if (virtualCode == null)
                        return false;
                    
                    if (virtualCode != data.VirtualCode)
                        return false;
                }
                
                if (original.Type != data.Type)
                    return false;
                
                if (original.Modifiers.HasFlag(ModifierKey.Alt) && !data.IsAlt)
                    return false;
                
                // What about other modifiers?

                return true;
            }, 
            args => 
            {
                args.Handled = true;
                instruction.Execute(macro);
            });
    }
}