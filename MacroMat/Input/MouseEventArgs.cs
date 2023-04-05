using System.ComponentModel;

namespace MacroMat.Input;

public class MouseEventArgs : HandledEventArgs
{
    public MouseEventData Data { get; }

    public MouseEventArgs(MouseEventData data)
    {
        Data = data;
    }
}