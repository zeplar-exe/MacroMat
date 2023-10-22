using MacroMat.Common;
using MacroMat.Extensions;
using MacroMat.Input;
using MacroMat.Instructions;
using NUnit.Framework;

namespace MacroMat.Tests;

[TestFixture]
public class Westeros
{
    [Test]
    public void Test1()
    {
        new Macro()
            .Wait(1500)
            .SimulateInput(KeyInputData.Press(Scancode.Of(InputKey.D)))
            .SimulateInput(KeyInputData.Release(Scancode.Of(InputKey.D)))
            .SimulateUnicode("Hello world!")
            .Wait(100)
        .Dispose();
    }

    [Test]
    public void Test2()
    {
        var macro = new Macro();
        var watcher = new InputWatcher(macro);

        watcher.AddKeyCallback(KeyInputData.Press(VirtualKey.Of(InputKey.F)), () =>
        {
            Console.WriteLine("LF");
        });

        macro.Wait(5000)
            .Dispose();
    }

    [Test]
    public void METHOD()
    {
        var macro = new Macro();
        var scroll = new SimulateMouseWheelInstruction(new MouseWheelInputData(1, 0));
        
        scroll.Execute(macro);
        macro.Wait(100).Dispose();
    }
}