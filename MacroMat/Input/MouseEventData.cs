namespace MacroMat.Input;

public abstract class MouseEventData
{
    public int PositionX { get; }
    public int PositionY { get; }

    protected MouseEventData(int positionX, int positionY)
    {
        PositionX = positionX;
        PositionY = positionY;
    }
}

public class MouseButtonEventData : MouseEventData
{
    public MouseButton Button { get; }
    public MouseButtonInputType Type { get; }

    public MouseButtonEventData(int positionX, int positionY, MouseButton button, MouseButtonInputType type)
        : base(positionX, positionY)
    {
        Button = button;
        Type = type;
    }
}

public class MouseMoveEventData : MouseEventData
{
    public MouseMoveEventData(int movePositionX, int movePositionY)
        : base(movePositionX, movePositionY)
    {
        
    }
}

public class MouseWheelEventData : MouseEventData
{
    public int DeltaX { get; }
    public int DeltaY { get; }

    public MouseWheelEventData(int positionX, int positionY, int deltaX, int deltaY) 
        : base(positionX, positionY)
    {
        DeltaX = deltaX;
        DeltaY = deltaY;
    }
}