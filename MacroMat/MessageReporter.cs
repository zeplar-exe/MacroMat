namespace MacroMat;

public class MessageReporter
{
    private List<MacroMessage> Messages { get; }

    public event EventHandler<MacroMessage>? OnMessage;
    public event EventHandler<MacroMessage>? OnError;

    public MessageReporter()
    {
        Messages = new List<MacroMessage>();
    }

    public void Log(string text)
    {
        var message = new MacroMessage(text, false);
        
        Messages.Add(message);
        OnMessage?.Invoke(this, message);
    }

    public void Error(string text)
    {
        var message = new MacroMessage(text, true);
        
        Messages.Add(message);
        OnMessage?.Invoke(this, message);
        OnError?.Invoke(this, message);
    }

    public IEnumerable<MacroMessage> EnumerateMessages()
    {
        return Messages;
    }
}