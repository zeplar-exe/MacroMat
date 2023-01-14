using MacroMat;
using MacroMat.Input;

var macro = new Macro()
    .OnKeyEvent(
        data => data.IsInjected, 
        data =>
        {
            Console.WriteLine(data.ToString());
        })
    .SimulateKey(InputKey.G, KeyInputType.KeyDown)
    .SimulateKey(InputKey.G, KeyInputType.KeyUp);

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