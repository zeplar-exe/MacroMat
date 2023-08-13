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
                var inputs = new List<Win32.INPUT>();
                
                foreach (var position in Positions)
                {
                    var input = new Win32.INPUT
                    {
                        type = Win32.InputType.INPUT_MOUSE,
                        U = new Win32.InputUnion
                        {
                            mi = new Win32.MOUSEINPUT
                            {
                                dwFlags = Win32.MOUSEEVENTF.MOVE,
                                dx = position.X,
                                dy = position.Y,
                                time = 0
                            }
                        }
                    };
                    
                    inputs.Add(input);
                }

                var result = Win32.SendInput((uint)inputs.Count, inputs.ToArray(), Win32.INPUT.Size);
                
                if (result != 1)
                {
                    WindowsHelper.HandleError(e => 
                        macro.Messages.Error($"Simulate Mouse Move Error: [{e.NativeErrorCode}] {e.Message}"));
                }
            }).Execute();
        });
    }
}