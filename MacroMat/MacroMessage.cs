namespace MacroMat;

/// <summary>
/// Log message used in <see cref="MessageReporter"/>.
/// </summary>
/// <param name="Message"></param>
/// <param name="IsError"></param>
public record MacroMessage(string Message, bool IsError);