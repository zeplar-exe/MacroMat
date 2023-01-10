namespace MacroMat.Tests;

public class Tests
{
    [Test]
    public async Task Test()
    {
        using var macro = new MacroListener();

        await Task.Delay(5000);
    }
}