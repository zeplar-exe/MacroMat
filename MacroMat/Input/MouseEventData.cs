namespace MacroMat.Input;

public class MouseEventData
{
    public MouseButton Button { get; }
    public MouseInputType Type { get; }

    public MouseEventData(MouseButton button, MouseInputType type)
    {
        Button = button;
        Type = type;
    }
}