using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.UI.Input.KeyboardAndMouse;
using MacroMat.Common;
using MacroMat.Input;
using MacroMat.SystemCalls.Windows;

namespace MacroMat.Instructions;

/// <summary>
/// Instruction to simulate a mouse click at its current position.
/// </summary>
public class SimulateMouseMoveInstruction : MacroInstruction
{
    private (int X, int Y)[] Positions { get; }

    /// <inheritdoc />
    public SimulateMouseMoveInstruction(int positionX, int positionY)
    {
        Positions = new[] { (positionX, positionY) };
    }

    public SimulateMouseMoveInstruction(IEnumerable<(int X, int Y)> positions)
    {
        Positions = positions.ToArray();
    }
    
    public SimulateMouseMoveInstruction(params (int X, int Y)[] positions)
    {
        Positions = positions.ToArray();
    }
    
    public SimulateMouseMoveInstruction(IEnumerable<int[]> positions)
    {
        Positions = positions.Select((array, i) =>
        {
            if (array.Length != 2)
                throw new ArgumentException($"Expected int array to contain exactly two integers (index:{i}, [ {string.Join(", ", array)} ])");

            return (array[0], array[1]);
        }).ToArray();
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
            }).Execute();
        });
    }
}