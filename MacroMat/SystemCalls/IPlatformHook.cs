namespace MacroMat.SystemCalls;

/// <summary>
/// Interface for platform hooks.
/// </summary>
internal interface IPlatformHook
{
    /// <summary>
    /// Method run when the corresponding platform's message loop begins.
    /// </summary>
    /// <returns>Whether initialization was successful.</returns>
    public bool Init(MessageLoop loop);
}