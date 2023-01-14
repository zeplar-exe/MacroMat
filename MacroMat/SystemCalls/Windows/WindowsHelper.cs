using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MacroMat.SystemCalls.Windows;

internal class WindowsHelper
{
    public static void HandleError(Action<Win32Exception> handler)
    {
        var exception = new Win32Exception(Marshal.GetLastWin32Error());
        
        handler.Invoke(exception);
    }
}