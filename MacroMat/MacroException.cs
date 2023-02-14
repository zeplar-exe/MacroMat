namespace MacroMat;

/// <summary>
/// Special exception used in <see cref="MessageReporter"/>.
/// </summary>
public class MacroException : Exception
{
    internal MacroException(string message) : base(message)
    {
        
    }
}