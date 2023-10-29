using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.UI.Input.KeyboardAndMouse;
using MacroMat.Common;
using MacroMat.Input;
using MacroMat.SystemCalls.Windows;

namespace MacroMat.Instructions;

/// <summary>
/// Instruction to simulate mouse wheel scrolling.
/// </summary>
public partial class SimulateMouseWheelInstruction : MacroInstruction
{
    private const uint WHEEL_DELTA = 120;
    
    private MouseWheelInputData Data { get; }

    public SimulateMouseWheelInstruction(MouseWheelInputData data)
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
                WindowsImplementation(macro);
            }).Execute();
        });
    }
}