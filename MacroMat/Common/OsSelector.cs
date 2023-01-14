namespace MacroMat.Common;

public class OsSelector
{
    private Action? Windows { get; set; }
    private Action? Mac { get; set; }
    private Action? Linux { get; set; }

    public OsSelector OnWindows(Action action)
    {
        Windows = action;

        return this;
    }

    public OsSelector OnMac(Action action)
    {
        Mac = action;

        return this;
    }

    public OsSelector OnLinux(Action action)
    {
        Linux = action;

        return this;
    }

    public void Execute()
    {
        if (OperatingSystem.IsWindows())
        {
            Windows?.Invoke();
        }
        else if (OperatingSystem.IsMacOS())
        {
            Mac?.Invoke();
        }
        else if (OperatingSystem.IsLinux())
        {
            Linux?.Invoke();
        }
    }
}

public class OsSelector<T> where T : class
{
    private Func<T>? Windows { get; set; }
    private Func<T>? Mac { get; set; }
    private Func<T>? Linux { get; set; }

    public OsSelector<T> OnWindows(Func<T> action)
    {
        Windows = action;

        return this;
    }

    public OsSelector<T> OnMac(Func<T> action)
    {
        Mac = action;

        return this;
    }

    public OsSelector<T> OnLinux(Func<T> action)
    {
        Linux = action;

        return this;
    }

    public T? Execute()
    {
        if (OperatingSystem.IsWindows())
        {
            return Windows?.Invoke();
        }
        else if (OperatingSystem.IsMacOS())
        {
            return Mac?.Invoke();
        }
        else if (OperatingSystem.IsLinux())
        {
            return Linux?.Invoke();
        }

        return default;
    }
}