using MacroMat;
using MacroMat.Extensions;
using MacroMat.Input;

var macro = new Macro()
    .OnKeyEvent(
        data => data.IsInjected, 
        data => Console.WriteLine("Injection: " + data))
    .Wait(2000)
    .SimulateKeys(new[] { InputKey.G, InputKey.A, InputKey.M, InputKey.E, InputKey.R }, KeyInputType.KeyDown)
    .SimulateKeys(new[] { InputKey.G, InputKey.A, InputKey.M, InputKey.E, InputKey.R }, KeyInputType.KeyUp);

macro.Messages.OnMessage += (sender, message) =>
{
    Console.WriteLine($"Message: {message.Message}");
};

Console.WriteLine("Press Q to execute all instructions, E to execute next instruction, X to exit...");

var atEnd = false;

while (true)
{
    var key = Console.ReadKey(true).Key;

    if (key == ConsoleKey.Q)
    {
        macro.ExecuteAll();
    }
    else if (key == ConsoleKey.E)
    {
        if (!macro.ExecuteNext())
        {
            if (!atEnd)
                Console.WriteLine("No more instructions to execute.");
            
            atEnd = true;
        }
    }
    else if (key == ConsoleKey.X)
    {
        break;
    }
}

Console.WriteLine("Exiting");

macro.Dispose();