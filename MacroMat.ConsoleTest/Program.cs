using MacroMat;
using MacroMat.Extensions;

var macro = new Macro()
    .OnMouseEvent(a => Console.WriteLine(a.Data.Button))
    .OnMouseEvent(a => Console.WriteLine(a.Data.Type));

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