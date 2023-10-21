using MacroMat.Common;
using MacroMat.Input;
using MacroMat.SystemCalls.Windows;

namespace MacroMat.Instructions;

/// <summary>
/// Instruction to simulate a key event.
/// </summary>
public class SimulateKeyboardInstruction : MacroInstruction
{
    private KeyInputData Data { get; }

    /// <inheritdoc />
    public SimulateKeyboardInstruction(KeyInputData data)
    {
        Data = data;
    }

    /// <inheritdoc />
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

                flags |= Win32.KEYBDEVENTF.SCANCODE;
                
                foreach (var key in Data.Keys)
                {
                    if (key is Scancode scancode)
                    {
                        var input = new Win32.INPUT
                        {
                            type = Win32.InputType.INPUT_KEYBOARD,
                            U = new Win32.InputUnion
                            {
                                ki = new Win32.KEYBDINPUT
                                {
                                    wScan = (Win32.WindowsScanCode)scancode.Value,
                                    dwFlags = flags,
                                    time = 0
                                }
                            }
                        };
                        
                        inputs.Add(input);
                    }
                    else if (key is VirtualKey virtualKey)
                    {
                        var virtualKeyScancode = Win32.MapVirtualKeyEx(
                            (uint)virtualKey.Value, 
                            (uint)Win32.MapVirtualKeyMapTypes.MAPVK_VK_TO_VSC, 
                            IntPtr.Zero);
                    
                        var input = new Win32.INPUT
                        {
                            type = Win32.InputType.INPUT_KEYBOARD,
                            U = new Win32.InputUnion
                            {
                                ki = new Win32.KEYBDINPUT
                                {
                                    wVk = (Win32.WindowsVirtualKey)virtualKey.Value,
                                    wScan = (Win32.WindowsScanCode)virtualKeyScancode,
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