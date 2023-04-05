using MacroMat;
using MacroMat.Extensions;
using MacroMat.Input;

var macro = new Macro()
    .RemapKey(
        InputData.FromKey(InputKey.Alt, KeyInputType.KeyDown), 
        InputData.FromKey(InputKey.Shift, KeyInputType.KeyDown))
    .RemapKey(
        InputData.FromKey(InputKey.Alt, KeyInputType.KeyUp), 
        InputData.FromKey(InputKey.Shift, KeyInputType.KeyUp));

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