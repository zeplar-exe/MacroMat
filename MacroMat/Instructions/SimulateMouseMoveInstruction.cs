using System.Drawing;
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
public partial class SimulateMouseMoveInstruction : MacroInstruction
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
                throw new ArgumentException($"Expected int array to contain exactly two integers (index: {i}, [ {string.Join(", ", array)} ])");

            return (array[0], array[1]);
        }).ToArray();
    }
    
    public SimulateMouseMoveInstruction(IEnumerable<Point> positions)
    {
        Positions = positions.Select((p, i) => (p.X, p.Y)).ToArray();
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
                WindowsImplementation(macro);
            }).Execute();
        });
    }
}