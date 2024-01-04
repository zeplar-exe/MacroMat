namespace MacroMat.SystemCalls.Linux;

public class LinuxMouseMoveEvent : EventArgs
{
    public LinuxMouseAxis Axis { get; }
    public int Amount { get; set; }
    
    public LinuxMouseMoveEvent(LinuxMouseAxis axis, int amount)
    {
        Axis = axis;
        Amount = amount;
    }
}