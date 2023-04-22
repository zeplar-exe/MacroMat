using MacroMat.Common;
using MacroMat.Input;
using MacroMat.SystemCalls.Windows;

namespace MacroMat.Instructions;

/// <summary>
/// Instruction to simulate a mouse click at its current position.
/// </summary>
public class SimulateMouseClickInstruction : MacroInstruction
{
    /// <summary>
    /// Mouse button to simulate.
    /// </summary>
    public MouseButton Button { get; }
    /// <summary>
    /// Mouse input type to simulate.
    /// </summary>
    public MouseInputType Type { get; }

    /// <inheritdoc />
    public SimulateMouseClickInstruction(MouseButton button, MouseInputType type)
    {
        Button = button;
        Type = type;
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
                var flags = (Win32.MOUSEEVENTF)0;

                flags |= (Button, Type) switch
                {
                    (MouseButton.Left, MouseInputType.Down) => Win32.MOUSEEVENTF.LEFTDOWN,
                    (MouseButton.Left, MouseInputType.Up) => Win32.MOUSEEVENTF.LEFTUP,
            
                    (MouseButton.Right, MouseInputType.Down) => Win32.MOUSEEVENTF.RIGHTDOWN,
                    (MouseButton.Right, MouseInputType.Up) => Win32.MOUSEEVENTF.RIGHTUP,
            
                    (MouseButton.MouseWheel, MouseInputType.Down) => Win32.MOUSEEVENTF.MIDDLEDOWN,
                    (MouseButton.MouseWheel, MouseInputType.Up) => Win32.MOUSEEVENTF.MIDDLEUP,
            
                    (MouseButton.MouseWheel, MouseInputType.WheelBackward) => Win32.MOUSEEVENTF.WHEEL,
                    (MouseButton.MouseWheel, MouseInputType.WheelForward) => Win32.MOUSEEVENTF.WHEEL,

                    _ => 0
                };

                var mouseData = Type switch
                {
                    MouseInputType.WheelBackward => -Win32.MOUSEINPUT.WHEEL_DELTA,
                    MouseInputType.WheelForward => Win32.MOUSEINPUT.WHEEL_DELTA,
                    _ => 0,
                };

                var input = new Win32.INPUT
                {
                    type = Win32.InputType.INPUT_MOUSE,
                    U = new Win32.InputUnion
                    {
                        mi = new Win32.MOUSEINPUT
                        {
                            mouseData = mouseData,
                            dwFlags = flags,
                            time = 0
                        }
                    }
                };

                var result = Win32.SendInput(1, new[] { input }, Win32.INPUT.Size);
                
                if (result != 1)
                {
                    WindowsHelper.HandleError(e => 
                        macro.Messages.Error($"Simulate Mouse Click Error: [{e.NativeErrorCode}] {e.Message}"));
                }
            }).Execute();
        });
    }
}