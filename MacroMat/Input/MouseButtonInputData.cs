using System.Collections.ObjectModel;

namespace MacroMat.Input;

public class MouseButtonInputData
{
    public IReadOnlyCollection<MouseButton> Buttons { get; }
    public MouseButtonInputType Type { get; }
    
    public MouseButtonInputData(MouseButton button, MouseButtonInputType type)
    {
        Buttons = new ReadOnlyCollection<MouseButton>(new[] { button });
        Type = type;
    }

    public MouseButtonInputData(IEnumerable<MouseButton> buttons, MouseButtonInputType type)
    {
        Buttons = buttons.ToList().AsReadOnly();
        Type = type;
    }
    
    public bool ContainsButton(MouseButton button)
    {
        return Buttons.Contains(button);
    }
}