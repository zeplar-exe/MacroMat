using MacroMat.Common;
using MacroMat.Input;
using MacroMat.SystemCalls.Windows;

namespace MacroMat.Instructions;

/// <summary>
/// Instruction to simulate mouse buttons at its current position.
/// </summary>
public class SimulateMouseInstruction : MacroInstruction
{
    private MouseInputData Data { get; }

    public SimulateMouseInstruction(MouseInputData data)
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
                var inputs = new List<Win32.INPUT>();
                var flags = (Win32.MOUSEEVENTF)0;

                foreach (var button in Data.Buttons)
                {
                    flags |= (button, Data.Type) switch
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

                    var mouseData = Data.Type switch
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
                    
                    inputs.Add(input);
                }
                
                var result = Win32.SendInput((uint)inputs.Count, inputs.ToArray(), Win32.INPUT.Size);

                if (result != 1)
                {
                    WindowsHelper.HandleError(e =>
                        macro.Messages.Error($"Simulate Mouse Click Error: [{e.NativeErrorCode}] {e.Message}"));
                }
            }).Execute();
        });
    }
}