using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.UI.Input.KeyboardAndMouse;
using MacroMat.Input;
using MacroMat.SystemCalls.Windows;

namespace MacroMat.Instructions;

public partial class SimulateMouseButtonInstruction
{
    private void WindowsImplementation(Macro macro)
    {
        var inputs = new INPUT[Data.Buttons.Count];
        var flags = (MOUSE_EVENT_FLAGS)0;
        var index = 0;

        foreach (var button in Data.Buttons)
        {
            flags |= (button, Data.Type) switch
            {
                (MouseButton.Left, MouseButtonInputType.Down) => MOUSE_EVENT_FLAGS.MOUSEEVENTF_LEFTDOWN,
                (MouseButton.Left, MouseButtonInputType.Up) => MOUSE_EVENT_FLAGS.MOUSEEVENTF_LEFTUP,

                (MouseButton.Right, MouseButtonInputType.Down) => MOUSE_EVENT_FLAGS.MOUSEEVENTF_RIGHTDOWN,
                (MouseButton.Right, MouseButtonInputType.Up) => MOUSE_EVENT_FLAGS.MOUSEEVENTF_RIGHTUP,

                (MouseButton.MouseWheel, MouseButtonInputType.Down) => MOUSE_EVENT_FLAGS.MOUSEEVENTF_MIDDLEDOWN,
                (MouseButton.MouseWheel, MouseButtonInputType.Up) => MOUSE_EVENT_FLAGS.MOUSEEVENTF_MIDDLEUP,

                _ => 0
            };

            var input = new INPUT
            {
                type = INPUT_TYPE.INPUT_MOUSE,
                Anonymous = new INPUT._Anonymous_e__Union()
                {
                    mi = new MOUSEINPUT
                    {
                        dwFlags = flags,
                        time = 0
                    }
                }
            };

            inputs[index++] = input;
        }

        unsafe
        {
            fixed (INPUT* inputPtr = &inputs[0])
            {
                var result = PInvoke.SendInput((uint)inputs.Length, inputPtr, Marshal.SizeOf<INPUT>());

                if (result != 1)
                {
                    WindowsHelper.HandleError(e =>
                        macro.Messages.Error($"Simulate Mouse Click Error: [{e.NativeErrorCode}] {e.Message}"));
                }
            }
        }
    }
}