using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.UI.Input.KeyboardAndMouse;
using Windows.Win32.UI.TextServices;
using MacroMat.Common;
using MacroMat.Input;
using MacroMat.SystemCalls.Windows;

namespace MacroMat.Instructions;

/// <summary>
/// Instruction to simulate a key event.
/// </summary>
public partial class SimulateKeyboardInstruction : MacroInstruction
{
    private KeyInputData Data { get; }

    /// <inheritdoc />
    public SimulateKeyboardInstruction(KeyInputData data)
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