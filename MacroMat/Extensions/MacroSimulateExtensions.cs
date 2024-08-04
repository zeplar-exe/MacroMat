using System.Drawing;
using MacroMat.Common;
using MacroMat.Input;
using MacroMat.Instructions;

namespace MacroMat.Extensions;

/// <summary>
/// Extension methods relating to simulated input instruction.
/// </summary>
public static class MacroSimulateExtensions
{
    public static Macro PressKey(this Macro macro, IKeyRepresentation key)
    {
        new SimulateKeyboardInstruction(KeyInputData.Press(key)).Execute(macro);
        
        return macro;
    }
    
    public static Macro ReleaseKey(this Macro macro, IKeyRepresentation key)
    {
        new SimulateKeyboardInstruction(KeyInputData.Release(key)).Execute(macro);

        return macro;
    }
    
    public static Macro TapKey(this Macro macro, IKeyRepresentation key)
    {
        macro.PressKey(key);
        macro.ReleaseKey(key);

        return macro;
    }
    
    /// <summary>
    /// Enqueue a SimulateKeyboardInstruction to simulate the specified input.
    /// </summary>
    /// <param name="macro"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static Macro SimulateKeyInput(this Macro macro, KeyInputData data)
    {
        new SimulateKeyboardInstruction(data).Execute(macro);
        
        return macro;
    }
    
    /// <summary>
    /// Enqueues SendUnicodeInstructions to write the specified unicode string once.
    /// </summary>
    /// <remarks>
    /// This method creates two SendUnicodeInstructions for KeyDown and KeyUp
    /// using a compound instruction.
    /// </remarks>
    public static Macro PressUnicode(this Macro macro, string s)
    {
        new SendUnicodeInstruction(s, KeyInputType.KeyDown).Execute(macro);
        
        return macro;
    }
    
    /// <summary>
    /// Enqueues SendUnicodeInstructions to write the specified unicode string once.
    /// </summary>
    /// <remarks>
    /// This method creates two SendUnicodeInstructions for KeyDown and KeyUp
    /// using a compound instruction.
    /// </remarks>
    public static Macro ReleaseUnicode(this Macro macro, string s)
    {
        new SendUnicodeInstruction(s, KeyInputType.KeyUp).Execute(macro);
        
        return macro;
    }
    
    /// <summary>
    /// Enqueues SendUnicodeInstructions to write the specified unicode string once.
    /// </summary>
    /// <remarks>
    /// This method creates two SendUnicodeInstructions for KeyDown and KeyUp
    /// using a compound instruction.
    /// </remarks>
    public static Macro TapUnicode(this Macro macro, string s)
    {
        macro.PressUnicode(s);
        macro.ReleaseUnicode(s);
        
        return macro;
    }

    public static Macro PressMouseButton(this Macro macro, MouseButton button)
    {
        new SimulateMouseButtonInstruction(new MouseButtonInputData(button, MouseButtonInputType.Down)).Execute(macro);
        
        return macro;
    }

    public static Macro ReleaseMouseButton(this Macro macro, MouseButton button)
    {
        new SimulateMouseButtonInstruction(new MouseButtonInputData(button, MouseButtonInputType.Up)).Execute(macro);
        
        return macro;
    }

    public static Macro TapMouseButton(this Macro macro, MouseButton button)
    {
        macro.PressMouseButton(button);
        macro.ReleaseMouseButton(button);

        return macro;
    }

    public static Macro SimulateMouseInput(this Macro macro, MouseButtonInputData data)
    {
        new SimulateMouseButtonInstruction(data).Execute(macro);
        
        return macro;
    }

    public static Macro Scroll(this Macro macro, int x, int y)
    {
        new SimulateMouseWheelInstruction(new MouseWheelInputData(y, x)).Execute(macro);
        
        return macro;
    }

    public static Macro ScrollVertical(this Macro macro, int y)
    {
        new SimulateMouseWheelInstruction(new MouseWheelInputData(y, 0)).Execute(macro);
        
        return macro;
    }

    public static Macro ScrollHorizontal(this Macro macro, int x)
    {
        new SimulateMouseWheelInstruction(new MouseWheelInputData(0, x)).Execute(macro);
        
        return macro;
    }
    
    public static Macro MoveMouse(this Macro macro, (int X, int Y) position)
    {
        new SimulateMouseMoveInstruction(position).Execute(macro);
        
        return macro;
    }

    public static Macro MoveMouse(this Macro macro, params (int X, int Y)[] positions)
    {
        new SimulateMouseMoveInstruction(positions).Execute(macro);
        
        return macro;
    }
    
    public static Macro MoveMouse(this Macro macro, Point position)
    {
        new SimulateMouseMoveInstruction(new[] { position }).Execute(macro);
        
        return macro;
    }
    
    public static Macro MoveMouse(this Macro macro, params Point[] positions)
    {
        new SimulateMouseMoveInstruction(positions).Execute(macro);
        
        return macro;
    }
}