using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.UI.Input.KeyboardAndMouse;
using MacroMat.Common;
using MacroMat.Input;
using MacroMat.SystemCalls.Windows;

namespace MacroMat.Instructions;

/// <summary>
/// Instruction to simulate mouse wheel scrolling.
/// </summary>
public class SimulateMouseWheelInstruction : MacroInstruction
{
    private const uint WHEEL_DELTA = 120;
    
    private MouseWheelInputData Data { get; }

    public SimulateMouseWheelInstruction(MouseWheelInputData data)
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
                var inputs = new INPUT[2];
                
                var veritcalInput = new INPUT
                {
                    type = INPUT_TYPE.INPUT_MOUSE,
                    Anonymous = new INPUT._Anonymous_e__Union
                    {
                        mi = new MOUSEINPUT
                        {
                            mouseData = (uint)(WHEEL_DELTA * Data.DeltaVertical),
                            dwFlags = MOUSE_EVENT_FLAGS.MOUSEEVENTF_WHEEL,
                            time = 0
                        }
                    }
                };
                
                var horizontalInput = new INPUT
                {
                    type = INPUT_TYPE.INPUT_MOUSE,
                    Anonymous = new INPUT._Anonymous_e__Union
                    {
                        mi = new MOUSEINPUT
                        {
                            mouseData = (uint)(WHEEL_DELTA * Data.DeltaHorizontal),
                            dwFlags = MOUSE_EVENT_FLAGS.MOUSEEVENTF_HWHEEL,
                            time = 0
                        }
                    }
                };
                
                inputs[0] = veritcalInput;
                inputs[1] = horizontalInput;

                unsafe
                {
                    fixed (INPUT* inputsPtr = &inputs[0])
                    {
                        var result = PInvoke.SendInput((uint)inputs.Length, inputsPtr, Marshal.SizeOf<INPUT>());

                        if (result != 1)
                        {
                            WindowsHelper.HandleError(e =>
                                macro.Messages.Error($"Simulate Mouse Click Error: [{e.NativeErrorCode}] {e.Message}"));
                        }
                    }
                }
            }).Execute();
        });
    }
}