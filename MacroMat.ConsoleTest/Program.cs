using MacroMat;
using MacroMat.Extensions;
using MacroMat.Input;

var macro = new Macro()
    .RemapKey(
        InputData.FromKey(InputKey.B, KeyInputType.KeyDown), 
        InputData.FromKey(InputKey.A, KeyInputType.KeyDown))
    .RemapKey(
        InputData.FromKey(InputKey.B, KeyInputType.KeyUp), 
        InputData.FromKey(InputKey.A, KeyInputType.KeyUp));

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