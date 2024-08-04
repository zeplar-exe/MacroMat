namespace MacroMat.SystemCalls.Linux;

public class LinuxKeyPressEvent : EventArgs
{
    public LinuxVirtualKey Code { get; }
    public LinuxKeyState State { get; }
    
    public LinuxKeyPressEvent(LinuxVirtualKey code, LinuxKeyState state)
    {
        Code = code;
        State = state;
    }
}