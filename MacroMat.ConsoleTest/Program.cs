using MacroMat;
using MacroMat.Input;
using MacroMat.Instructions;

var macro = new Macro();

macro.Messages.OnMessage += (sender, message) =>
{
    Console.WriteLine($"Message: {message.Message}");
};

var simulate = new SimulateKeyInstruction(InputKey.A, KeyInputType.KeyDown);
macro.AddInstruction(simulate);

Console.WriteLine("Press A to execute next instruction, X to exit...");

while (true)
{
    var key = Console.ReadKey(true).Key;

    if (key == ConsoleKey.A)
    {
        if (!macro.ExecuteNext())
            Console.WriteLine("No more instructions to execute.");
    }
    else if (key == ConsoleKey.X)
    {
        break;
    }
}

Console.WriteLine("Exiting");

macro.Dispose();