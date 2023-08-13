using System.Collections.ObjectModel;

namespace MacroMat.Input;

public class MouseInputData
{
    public IReadOnlyCollection<MouseButton> Buttons { get; }
    public MouseInputType Type { get; }
    
    public MouseInputData(MouseButton button, MouseInputType type)
    {
        Buttons = new ReadOnlyCollection<MouseButton>(new[] { button });
        Type = type;
    }

    public MouseInputData(IEnumerable<MouseButton> buttons, MouseInputType type)
    {
        Buttons = buttons.ToList().AsReadOnly();
        Type = type;
    }
    
    public bool ContainsButton(MouseButton button)
    {
        return Buttons.Contains(button);
    }
}