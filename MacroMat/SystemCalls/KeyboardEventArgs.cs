using System.ComponentModel;

namespace MacroMat.SystemCalls;

public class KeyboardEventArgs : HandledEventArgs
{
    public KeyboardEventData Data { get; }

    public KeyboardEventArgs(KeyboardEventData data)
    {
        Data = data;
    }
}