using System.ComponentModel;

namespace MacroMat.Input;

/// <summary>
/// HandledEventArgs for <see cref="KeyboardEventData"/>.
/// </summary>
public class KeyboardEventArgs : HandledEventArgs
{
    /// <summary>
    /// Keyboard event data... yeah.
    /// </summary>
    public KeyboardEventData Data { get; }

    /// <inheritdoc />
    public KeyboardEventArgs(KeyboardEventData data)
    {
        Data = data;
    }
}