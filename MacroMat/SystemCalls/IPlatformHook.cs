namespace MacroMat.SystemCalls;

/// <summary>
/// Interface for platform hooks.
/// </summary>
internal interface IPlatformHook
{
    /// <summary>
    /// Initializes a message loop for the implemented platform/OS.
    /// </summary>
    /// <returns>Whether the message loop was successfully initialized.</returns>
    public bool MessageLoopInit();
}