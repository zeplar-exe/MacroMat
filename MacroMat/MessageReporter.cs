namespace MacroMat;

/// <summary>
/// Logging mechanism in <see cref="Macro"/>.
/// </summary>
public class MessageReporter
{
    private List<MacroMessage> Messages { get; }

    /// <summary>
    /// Event invoked whenever a message is logged, error or not.
    /// </summary>
    public event EventHandler<MacroMessage>? OnMessage;
    /// <summary>
    /// Event invoked whenever an error message is logged.
    /// </summary>
    public event EventHandler<MacroMessage>? OnError;

    internal MessageReporter()
    {
        Messages = new List<MacroMessage>();
    }

    /// <summary>
    /// Log a regular message, invoking <see cref="OnMessage"/>.
    /// </summary>
    public void Log(string text)
    {
        var message = new MacroMessage(text, false);
        
        Messages.Add(message);
        OnMessage?.Invoke(this, message);
    }

    /// <summary>
    /// Log an error message, invoking <see cref="OnMessage"/> and <see cref="OnError"/>.
    /// </summary>
    public void Error(string text)
    {
        var message = new MacroMessage(text, true);
        
        Messages.Add(message);
        OnMessage?.Invoke(this, message);
        OnError?.Invoke(this, message);
    }

    /// <summary>
    /// Enumerate all logged messages.
    /// </summary>
    public IEnumerable<MacroMessage> EnumerateMessages()
    {
        return Messages.AsEnumerable();
    }
}