using MacroMat.Common;
using MacroMat.Input;
using MacroMat.SystemCalls.Windows;

namespace MacroMat.Instructions;

/// <summary>
/// Instruction to simulate a mouse click at its current position.
/// </summary>
public class SimulateMouseMoveInstruction : MacroInstruction
{
    /// <summary>
    /// Mouse button to simulate.
    /// </summary>
    public MouseButton Button { get; }
    /// <summary>
    /// Screen x (left->right) position (usually in pixels) to move the mouse to.
    /// </summary>
    public int PositionX { get; }
    /// <summary>
    /// Screen y (top->down) position (usually in pixels) to move the mouse to.
    /// </summary>
    public int PositionY { get; }

    /// <inheritdoc />
    public SimulateMouseMoveInstruction(MouseButton button, int positionX, int positionY)
    {
        Button = button;
        PositionX = positionX;
        PositionY = positionY;
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
                var input = new Win32.INPUT
                {
                    type = Win32.InputType.INPUT_MOUSE,
                    U = new Win32.InputUnion
                    {
                        mi = new Win32.MOUSEINPUT
                        {
                            dwFlags = Win32.MOUSEEVENTF.MOVE,
                            dx = PositionX,
                            dy = PositionY,
                            time = 0
                        }
                    }
                };

                var result = Win32.SendInput(1, new[] { input }, Win32.INPUT.Size);
                
                if (result != 1)
                {
                    WindowsHelper.HandleError(e => 
                        macro.Messages.Error($"Simulate Mouse Move Error: [{e.NativeErrorCode}] {e.Message}"));
                }
            }).Execute();
        });
    }
}