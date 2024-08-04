using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.UI.Input.KeyboardAndMouse;
using MacroMat.SystemCalls.Windows;

namespace MacroMat.Instructions;

public partial class SimulateMouseMoveInstruction
{
    [DllImport("User32.dll",
        EntryPoint = "GetSystemMetrics",
        CallingConvention = CallingConvention.Winapi)]
    internal static extern int GetSystemMetrics(int value);
    
    private void WindowsImplementation(Macro macro)
    {
        var inputs = new INPUT[Positions.Length];

        int screenWidth = GetSystemMetrics(0);
        int screenHeight = GetSystemMetrics(1);

        for (var index = 0; index < Positions.Length; index++)
        {
            var position = Positions[index];
            position.X = (int)Math.Round((double)position.X * 0xFFFF / screenWidth);
            position.Y = (int)Math.Round((double)position.Y * 0xFFFF / screenHeight);
            
            var input = new INPUT
            {
                type = INPUT_TYPE.INPUT_MOUSE,
                Anonymous = new INPUT._Anonymous_e__Union
                {
                    mi = new MOUSEINPUT
                    {
                        dwFlags = MOUSE_EVENT_FLAGS.MOUSEEVENTF_MOVE | MOUSE_EVENT_FLAGS.MOUSEEVENTF_ABSOLUTE,
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