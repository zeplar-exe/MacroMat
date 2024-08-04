using MacroMat.Common;
using MacroMat.Input;
using MacroMat.Instructions;

namespace MacroMat.Extensions;

/// <summary>
/// Extension methods relating to macro timeouts and delays.
/// </summary>
public static class MacroWaitExtensions
{
    /// <summary>
    /// Enqueue a WaitInstruction to wait for the specified amount of milliseconds.
    /// </summary>
    public static Macro Wait(this Macro macro, int milliseconds)
    {
        new WaitInstruction(TimeSpan.FromMilliseconds(milliseconds)).Execute(macro);
        
        return macro;
    }
    
    /// <summary>
    /// Enqueue a WaitInstruction to wait for the specified amount of time.
    /// </summary>
    public static Macro Wait(this Macro macro, TimeSpan time)
    {
        new WaitInstruction(time).Execute(macro);
        
        return macro;
    }
    
    public static Macro WaitForKeyPressed(this Macro macro, IKeyRepresentation key, int waitInterval = 100)
    {
        var done = false;

        macro.OnKeyPressed(key, _ => done = true);

        while (!done)
            macro.Wait(waitInterval);

        return macro;
    }

    public static Macro WaitForKeyReleased(this Macro macro, IKeyRepresentation key, int waitInterval = 100)
    {
        var done = false;

        macro.OnKeyReleased(key, _ => done = true);

        while (!done)
            macro.Wait(waitInterval);

        return macro;
    }

    public static Macro WaitForMouseButtonPressed(this Macro macro, MouseButton button, int waitInterval = 100)
    {
        var done = false;

        macro.OnMouseButtonPressed(button, (_, _) => done = true);

        while (!done)
            macro.Wait(waitInterval);

        return macro;
    }
    
    public static Macro WaitForMouseButtonReleased(this Macro macro, MouseButton button, int waitInterval = 100)
    {
        var done = false;

        macro.OnMouseButtonReleased(button, (_, _) => done = true);

        while (!done)
            macro.Wait(waitInterval);

        return macro;
    }
    
    public static Macro WaitForMouseMove(this Macro macro, int verticalAmount = 0, int horizontalAmount = 0, int waitInterval = 100)
    {
        var done = false;

        macro.OnMouseMove((_, _) => done = true);

        while (!done)
            macro.Wait(waitInterval);

        return macro;
    }
    
    public static Macro WaitForMouseScroll(this Macro macro, int verticalAmount = 0, int horizontalAmount = 0, int waitInterval = 100)
    {
        var done = false;

        macro.OnMouseScroll((_, _) => done = true);

        while (!done)
            macro.Wait(waitInterval);

        return macro;
    }
}