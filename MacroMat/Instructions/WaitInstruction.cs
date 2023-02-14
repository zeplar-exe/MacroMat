namespace MacroMat.Instructions;

/// <summary>
/// Instruction to pause the current thread for a specified amount of time.
/// </summary>
public class WaitInstruction : MacroInstruction
{
    /// <summary>
    /// <see cref="TimeSpan"/> to wait.
    /// </summary>
    public TimeSpan Time { get; }

    /// <inheritdoc />
    public WaitInstruction(TimeSpan time)
    {
        Time = time;
    }

    /// <inheritdoc />
    public override void Execute(Macro macro)
    {
        Thread.Sleep(Time);
    }
}