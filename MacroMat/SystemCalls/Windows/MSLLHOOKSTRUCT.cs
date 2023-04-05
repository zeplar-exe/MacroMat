using System.Runtime.InteropServices;

namespace MacroMat.SystemCalls.Windows;

[StructLayout(LayoutKind.Sequential)]
internal struct MSLLHOOKSTRUCT
{
    public Win32.POINT pt;
    public int mouseData; // be careful, this must be ints, not uints (was wrong before I changed it...). regards, cmew.
    public int flags;
    public int time;
    public UIntPtr dwExtraInfo;
}
