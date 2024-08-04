using MacroMat.Common;
using MacroMat.Input;

namespace MacroMat.Instructions;

/// <summary>
/// Instruction to simulate mouse buttons at its current position.
/// </summary>
public partial class SimulateMouseButtonInstruction : MacroInstruction
{
    private MouseButtonInputData Data { get; }

    public SimulateMouseButtonInstruction(MouseButtonInputData data)
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

            os.OnWindows(() => { WindowsImplementation(macro); }).Execute();
        });
    }
}