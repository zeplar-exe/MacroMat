using CommandDotNet;

public class Program
{
    public static int Main(string[] args)
    {
        return new AppRunner<Program>().Run(args);
    }

    [DefaultCommand]
    [Command("run")]
    public int Run(string file)
    {
        if (!RequireFileExists(file))
            return 1;

        return 0;
    }

    [Command("debug")]
    public int Debug(string file)
    {
        if (!RequireFileExists(file))
            return 1;

        return 0;
    }

    private bool RequireFileExists(string file)
    {
        if (!File.Exists(file))
        {
            file = Path.Join(Directory.GetCurrentDirectory(), file);

            if (!File.Exists(file))
            {
                Console.WriteLine($"The given file ({file}) was not found.");
                
                return false;
            }
        }

        return true;
    }
}