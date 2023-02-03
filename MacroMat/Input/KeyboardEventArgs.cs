using System.ComponentModel;

namespace MacroMat.Input;

/// <summary>
/// HandledEventArgs for <see cref="KeyboardEventData"/>.
/// </summary>
public class KeyboardEventArgs : HandledEventArgs
{
    public KeyboardEventData Data { get; }

    /// <inheritdoc />
    public KeyboardEventArgs(KeyboardEventData data)
    {
        Data = data;
    }
}