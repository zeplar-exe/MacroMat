using MacroMat;

var listener = new MacroListener();

Console.WriteLine("Press X key to exit...");

while (Console.ReadKey(true).Key != ConsoleKey.X)
{
    
}

Console.WriteLine("Exiting");

listener.Dispose();