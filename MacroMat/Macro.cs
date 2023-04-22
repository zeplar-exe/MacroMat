using MacroMat.Instructions;

namespace MacroMat;

/// <summary>
/// The core class for building and executing macros.
/// </summary>
public sealed class Macro : IDisposable
{
    internal MacroListener Listener { get; }
    private Queue<MacroInstruction> Instructions { get; }
    
    /// <summary>
    /// Log messages created by the macro and its internal processes.
    /// </summary>
    public MessageReporter Messages { get; }
    
    /// <summary>
    /// Whether this macro has been initialized by calling <see cref="Initialize"/>.
    /// </summary>
    public bool IsInitialized { get; private set; }

    /// <summary>
    /// Create a new Macro.
    /// </summary>
    /// <param name="autoInitialize">Whether to call <see cref="Initialize"/>
    /// after construction. Defaults to true.</param>
    public Macro(bool autoInitialize = true)
    {
        Messages = new MessageReporter();
        Listener = new MacroListener(Messages);
        Instructions = new Queue<MacroInstruction>();

        if (autoInitialize)
            Initialize();
    }

    /// <summary>
    /// Initializes any expensive or macro-specific internals.
    /// </summary>
    /// <remarks>
    /// To be specific, the following internal values are initialized:
    /// <list type="bullet">
    /// <item>MacroListener - An OS-specific process to handle message loops and input events.</item>
    /// </list>
    /// </remarks>
    public void Initialize()
    {
        if (IsInitialized)
            return;
        
        IsInitialized = true;
        Listener.Start();
    }

    /// <summary>
    /// Enqueue a new instruction for execution FIFO style.
    /// </summary>
    /// <param name="instruction"></param>
    public Macro AddInstruction(MacroInstruction instruction)
    {
        Instructions.Enqueue(instruction);

        return this;
    }
    
    /// <summary>
    /// Execute all queued instructions one-by-one.
    /// </summary>
    public void ExecuteAll()
    {
        while (ExecuteNext())
        {
            
        }
    }

    /// <summary>
    /// Execute the next queued instruction.
    /// </summary>
    /// <returns>Whether an instruction was executed; that is,
    /// a value of false indicates no more instructions to execute.</returns>
    public bool ExecuteNext()
    {
        if (Instructions.TryDequeue(out var instruction))
        {
            instruction.Execute(this);

            return true;
        }

        return false;
    }

    public void Clean()
    {
        
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Listener.Dispose();
    }
}