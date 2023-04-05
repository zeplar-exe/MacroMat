using MacroMat;
using MacroMat.Extensions;
using MacroMat.Input;

var macro = new Macro()
    .RemapKey(
        InputData.FromKey(0, KeyInputType.KeyDown, ModifierKey.Alt), 
        InputData.FromKey(0, KeyInputType.KeyDown, ModifierKey.Shift))
    .RemapKey(
        InputData.FromKey(0, KeyInputType.KeyUp, ModifierKey.Alt), 
        InputData.FromKey(0, KeyInputType.KeyUp, ModifierKey.Shift));

macro.Messages.OnMessage += (sender, message) =>
{
    Console.WriteLine($"Message: {message.Message}");
};

macro.ExecuteAll();

Console.WriteLine("Press X to exit...");

while (true)
{
    var key = Console.ReadKey(true).Key;

    if (key == ConsoleKey.X)
    {
        break;
    }
}

Console.WriteLine("Exiting");

macro.Dispose();