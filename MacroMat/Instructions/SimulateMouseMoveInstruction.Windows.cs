using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.UI.Input.KeyboardAndMouse;
using MacroMat.SystemCalls.Windows;

namespace MacroMat.Instructions;

public partial class SimulateMouseMoveInstruction
{
    private void WindowsImplementation(Macro macro)
    {
        var inputs = new INPUT[Positions.Length];

        for (var index = 0; index < Positions.Length; index++)
        {
            var position = Positions[index];
            var input = new INPUT
            {
                type = INPUT_TYPE.INPUT_MOUSE,
                Anonymous = new INPUT._Anonymous_e__Union
                {
                    mi = new MOUSEINPUT
                    {
                        dwFlags = MOUSE_EVENT_FLAGS.MOUSEEVENTF_MOVE,
                        dx = position.X,
                        dy = position.Y,
                        time = 0
                    }
                }
            };

            inputs[index] = input;
        }

        unsafe
        {
            fixed (INPUT* inputsPtr = &inputs[0])
            {
                var result = PInvoke.SendInput((uint)inputs.Length, inputsPtr, Marshal.SizeOf<INPUT>());

                if (result != 1)
                {
                    WindowsHelper.HandleError(e =>
                        macro.Messages.Error($"Simulate Mouse Move Error: [{e.NativeErrorCode}] {e.Message}"));
                }
            }
        }
    }
}