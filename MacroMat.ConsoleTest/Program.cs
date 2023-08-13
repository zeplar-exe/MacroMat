using MacroMat;
using MacroMat.Extensions;
using MacroMat.Input;
using MacroMat.Instructions;

var backslash = InputKeyTranslator.ToWindowsScancode(InputKey.A)!;

var macro = new Macro()
    .Wait(1500)
    .EnqueueInstruction(new SimulateMouseMoveInstruction((0, 0), (100, 100), (200, 50)));

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