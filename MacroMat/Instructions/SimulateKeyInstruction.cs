using System.ComponentModel;
using System.Runtime.InteropServices;

using MacroMat.Common;
using MacroMat.Input;
using MacroMat.SystemCalls.Windows;

namespace MacroMat.Instructions;

public class SimulateKeyInstruction : MacroInstruction
{
    private InputData Data { get; }
    
    public SimulateKeyInstruction(InputData data)
    {
        Data = data;
    }

    public override void Execute(Macro macro)
    {
        var loop = macro.Listener.MessageLoop;
        
        loop?.EnqueueAction(() =>
        {
            var os = new OsSelector();

            os.OnWindows(() =>
            {
                var flags = (Win32.KEYBDEVENTF)0;

                if (Data.Type == KeyInputType.KeyUp)
                    flags |= Win32.KEYBDEVENTF.KEYUP;

                var inputs = new List<Win32.INPUT>();

                if (Data.IsScancode)
                {
                    flags |= Win32.KEYBDEVENTF.SCANCODE;
                    
                    foreach (var scancode in Data.Scancodes)
                    {
                        var input = new Win32.INPUT
                        {
                            type = Win32.InputType.INPUT_KEYBOARD,
                            U = new Win32.InputUnion
                            {
                                ki = new Win32.KEYBDINPUT
                                {
                                    wScan = (Win32.WindowsScanCode)scancode,
                                    dwFlags = flags,
                                    time = 0
                                }
                            }
                        };
                        
                        inputs.Add(input);
                    }
                }
                else
                {
                    var virtualKeys = new List<Win32.WindowsVirtualKey>();
                    
                    foreach (var key in Data.InputKeys)
                    {
                        var virtualKey = InputKeyTranslator.ToWindowsVirtual(key);

                        if (virtualKey == null)
                        {
                            macro.Messages.Log($"'{key}' does not have a Windows virtual key equivalent.");
                        
                            continue;
                        }
                    
                        virtualKeys.Add(virtualKey.Value);
                    }
                    
                    foreach (var virtualKey in virtualKeys)
                    {
                        var scancode = Win32.MapVirtualKeyEx(
                            (uint)virtualKey, 
                            (uint)Win32.MapVirtualKeyMapTypes.MAPVK_VK_TO_VSC, 
                            IntPtr.Zero);
                        
                        var input = new Win32.INPUT
                        {
                            type = Win32.InputType.INPUT_KEYBOARD,
                            U = new Win32.InputUnion
                            {
                                ki = new Win32.KEYBDINPUT
                                {
                                    wVk = virtualKey,
                                    wScan = (Win32.WindowsScanCode)scancode,
                                    dwFlags = flags,
                                    time = 0
                                }
                            }
                        };
                        
                        inputs.Add(input);
                    }
                }

                // Note to self:
                // if this every mysteriously stops working, make sure your enums are the correct numeric type
                // an int enum where there should be a byte messes things up

                var items = inputs.ToArray();
                var result = Win32.SendInput((uint)items.Length, items, Win32.INPUT.Size);

                if (result != items.Length)
                {
                    WindowsHelper.HandleError(e => 
                        macro.Messages.Error($"Simulate Key Error: [{e.NativeErrorCode}] {e.Message}"));
                }
            }).Execute();
        });
    }
}