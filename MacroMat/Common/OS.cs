namespace MacroMat.Common;

public class OS
{
    private Action? Windows { get; set; }
    private Action? Mac { get; set; }
    private Action? Linux { get; set; }

    public OS OnWindows(Action action)
    {
        Windows = action;

        return this;
    }

    public OS OnMac(Action action)
    {
        Mac = action;

        return this;
    }

    public OS OnLinux(Action action)
    {
        Linux = action;

        return this;
    }

    public void Execute()
    {
        switch (Environment.OSVersion.Platform)
        {
            case PlatformID.Win32NT:
                Windows?.Invoke();
                break;
            case PlatformID.MacOSX:
                Mac?.Invoke();
                break;
            case PlatformID.Unix:
                Linux?.Invoke();
                break;
        }
    }
}