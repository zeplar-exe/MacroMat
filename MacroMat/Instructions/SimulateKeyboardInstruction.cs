using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.UI.Input.KeyboardAndMouse;
using Windows.Win32.UI.TextServices;
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
                var flags = (KEYBD_EVENT_FLAGS)0;

                if (Data.Type == KeyInputType.KeyUp)
                    flags |= KEYBD_EVENT_FLAGS.KEYEVENTF_KEYUP;

                var inputs = new INPUT[Data.Keys.Count];
                var index = 0;

                flags |= KEYBD_EVENT_FLAGS.KEYEVENTF_SCANCODE;
                
                foreach (var key in Data.Keys)
                {
                    if (key is Scancode scancode)
                    {
                        var input = new INPUT
                        {
                            type = INPUT_TYPE.INPUT_KEYBOARD,
                            Anonymous = new INPUT._Anonymous_e__Union
                            {
                                ki = new KEYBDINPUT
                                {
                                    wScan = scancode.Value,
                                    dwFlags = flags,
                                    time = 0
                                }
                            }
                        };
                        
                        inputs[index++] = input;
                    }
                    else if (key is VirtualKey virtualKey)
                    {
                        var virtualKeyScancode = PInvoke.MapVirtualKeyEx(
                            virtualKey.Value, 
                            (uint)MAP_VIRTUAL_KEY_TYPE.MAPVK_VK_TO_VSC, 
                            new HKL(IntPtr.Zero));
                    
                        var input = new INPUT
                        {
                            type = INPUT_TYPE.INPUT_KEYBOARD,
                            Anonymous = new INPUT._Anonymous_e__Union
                            {
                                ki = new KEYBDINPUT
                                {
                                    wVk = (VIRTUAL_KEY)virtualKey.Value,
                                    wScan = (ushort)virtualKeyScancode,
                                    dwFlags = flags,
                                    time = 0
                                }
                            }
                        };
                        
                        inputs[index++] = input;
                    }
                }

                // Note to self:
                // if this every mysteriously stops working, make sure your enums are the correct numeric type
                // an int enum where there should be a byte messes things up

                unsafe
                {
                    fixed (INPUT* inputPtr = &inputs[0])
                    {
                        var items = inputs.ToArray();
                        var result = PInvoke.SendInput((uint)items.Length, inputPtr, Marshal.SizeOf<INPUT>());

                        if (result != items.Length)
                        {
                            WindowsHelper.HandleError(e => 
                                macro.Messages.Error($"Simulate Key Error: [{e.NativeErrorCode}] {e.Message}"));
                        }
                    }
                }
            }).Execute();
        });
    }
}