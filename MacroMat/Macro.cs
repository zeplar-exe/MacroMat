using MacroMat.Instructions;

namespace MacroMat;

public sealed class Macro : IDisposable
{
    internal MacroListener Listener { get; }
    private Queue<MacroInstruction> Instructions { get; }

    public Macro()
    {
        Listener = new MacroListener();
        Instructions = new Queue<MacroInstruction>();
    }

    public void ExecuteNext()
    {
        if (Instructions.TryDequeue(out var instruction))
        {
            instruction.Execute(this);
        }
    }

    public void Dispose()
    {
        Listener.Dispose();
    }
}