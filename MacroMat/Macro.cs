namespace MacroMat;

/// <summary>
/// The core class for building and executing macros.
/// </summary>
public sealed class Macro : IDisposable
{
    internal MacroListener Listener { get; }
    
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

        if (autoInitialize)
            Initialize();
    }

    /// <summary>
    /// Initialize expensive internals.
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

    ~Macro()
    {
        Dispose(true);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    public void Dispose(bool disposing = true)
    {
        Listener.Dispose(disposing);
    }
}