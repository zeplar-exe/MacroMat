using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.UI.Input.KeyboardAndMouse;
using MacroMat.SystemCalls.Windows;

namespace MacroMat.Instructions;

public partial class SimulateMouseWheelInstruction
{
    private void WindowsImplementation(Macro macro)
    {
        var inputs = new INPUT[2];

        var verticalInput = new INPUT
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

        inputs[0] = verticalInput;
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
    }
}