using System.ComponentModel;
using System.Runtime.InteropServices;

using MacroMat.Common;
using MacroMat.Input;
using MacroMat.SystemCalls.Windows;

namespace MacroMat.Instructions;

public class SimulateKeyInstruction : MacroInstruction
{
    public InputKey Key { get; }
    public KeyInputType Type { get; }

    public SimulateKeyInstruction(InputKey key, KeyInputType type)
    {
        Key = key;
        Type = type;
    }

    public override void Execute(Macro macro)
    {
        var loop = macro.Listener.MessageLoop;
        
        loop?.EnqueueAction(() =>
        {
            var os = new OsSelector();

            os.OnWindows(() =>
            {
                var input = new Win32.INPUT
                {
                    type = Win32.InputType.INPUT_KEYBOARD, 
                    U = new Win32.InputUnion
                    {
                        ki = new Win32.KEYBDINPUT
                        {
                            wVk = Win32.WindowsVirtualKey.KEY_A,
                            time = 0,  
                        }
                    }
                };
                
                var items = new[] { input };
                
                var result = Win32.SendInput(1, items, Win32.INPUT.Size);

                if (result != items.Length)
                {
                    WindowsHelper.HandleError(e => 
                        macro.Messages.Error($"Simulate Input Error: [{e.NativeErrorCode}] {e.Message}"));
                }
            }).Execute();
        });
    }
}