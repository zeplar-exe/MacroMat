using MacroMat;

var listener = new MacroListener();

listener.Start();

Console.WriteLine("Press X key to exit...");

while (Console.ReadKey(true).Key != ConsoleKey.X)
{
    
}

Console.WriteLine("Exiting");

listener.Dispose();