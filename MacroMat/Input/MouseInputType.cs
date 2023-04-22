namespace MacroMat.Input;

/// <summary>
/// Type of mouse input in a simulation or event.
/// </summary>
public enum MouseInputType
{
    /// <summary>
    /// Represents a mouse button as pressed.
    /// </summary>
    Down,
    /// <summary>
    /// Represents a mouse button as released.
    /// </summary>
    Up,
    WheelBackward,
    WheelForward,
}