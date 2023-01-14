namespace MacroMat.Instructions;

public class WaitInstruction : MacroInstruction
{
    public TimeSpan Time { get; }

    public WaitInstruction(TimeSpan time)
    {
        Time = time;
    }

    public override void Execute(Macro macro)
    {
        Thread.Sleep(Time);
    }
}