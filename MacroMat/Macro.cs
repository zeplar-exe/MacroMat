using MacroMat.Instructions;

namespace MacroMat;

public sealed class Macro : IDisposable
{
    internal MacroListener Listener { get; }
    private Queue<MacroInstruction> Instructions { get; }
    
    public MessageReporter Messages { get; }

    public Macro()
    {
        Messages = new MessageReporter();
        Listener = new MacroListener(Messages);
        Instructions = new Queue<MacroInstruction>();
        
        Listener.Start();
    }

    public void AddInstruction(MacroInstruction instruction)
    {
        Instructions.Enqueue(instruction);
    }

    public bool ExecuteNext()
    {
        if (Instructions.TryDequeue(out var instruction))
        {
            instruction.Execute(this);

            return true;
        }

        return false;
    }

    public void Dispose()
    {
        Listener.Dispose();
    }
}