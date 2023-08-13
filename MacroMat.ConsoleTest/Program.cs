using MacroMat;
using MacroMat.Extensions;
using MacroMat.Input;

var backslash = InputKeyTranslator.ToWindowsScancode(InputKey.A)!;

var macro = new Macro()
    .Wait(1500)
    .SimulateInput(KeyInputData.FromScancode(backslash.Value, KeyInputType.KeyDown))
    .Wait(1000)
    .SimulateInput(KeyInputData.FromScancode(backslash.Value, KeyInputType.KeyUp));

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