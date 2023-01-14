using MacroMat.Input;
using MacroMat.Instructions;
using MacroMat.SystemCalls;

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

    public Macro Wait(TimeSpan time)
    {
        return AddInstruction(new WaitInstruction(time));
    }
    
    public Macro SimulateScancode(uint scancode, KeyInputType type)
    {
        return AddInstruction(new SimulateKeyInstruction(scancode, type));
    }

    public Macro SimulateKey(InputKey key, KeyInputType type)
    {
        return AddInstruction(new SimulateKeyInstruction(key, type));
    }
    
    public Macro OnKeyEvent(Action<KeyboardEventData> action)
    {
        return OnKeyEvent(_ => true, action);
    }

    public Macro OnKeyEvent(Func<KeyboardEventData, bool> predicate, Action<KeyboardEventData> action)
    {
        return AddInstruction(new KeyCallbackInstruction(predicate, action));
    }

    public Macro AddInstruction(MacroInstruction instruction)
    {
        Instructions.Enqueue(instruction);

        return this;
    }
    
    public void ExecuteAll()
    {
        while (ExecuteNext())
        {
            
        }
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