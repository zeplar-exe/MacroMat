namespace MacroMat;

public sealed class Macro : IDisposable
{
    private MacroListener Listener { get; }

    public Macro()
    {
        Listener = new MacroListener();
    }

    public void Dispose()
    {
        Listener.Dispose();
    }
}