using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.UI.Input.KeyboardAndMouse;
using MacroMat.Input;
using MacroMat.SystemCalls.Windows;

namespace MacroMat.Instructions;

public partial class SendUnicodeInstruction
{
    private void WindowsImplementation(Macro macro)
    {
        var inputs = new INPUT[UnicodeString.Length];
        var flags = KEYBD_EVENT_FLAGS.KEYEVENTF_UNICODE;

        if (Type == KeyInputType.KeyUp)
            flags |= KEYBD_EVENT_FLAGS.KEYEVENTF_KEYUP;

        var index = 0;
                
        foreach (var code in UnicodeString.Select(Convert.ToUInt16))
        {
            var input = new INPUT
            {
                type = INPUT_TYPE.INPUT_KEYBOARD,
                Anonymous = new INPUT._Anonymous_e__Union
                {
                    ki = new KEYBDINPUT
                    {
                        wVk = 0,
                        wScan = code,
                        dwFlags = flags,
                        time = 0
                    }
                }
            };
                        
            inputs[index++] = input;
        }

        unsafe
        {
            fixed (INPUT* inputPtr = &inputs[0])
            {
                var items = inputs.ToArray();
                var result = PInvoke.SendInput((uint)items.Length, inputPtr, Marshal.SizeOf<INPUT>());

                if (result != items.Length)
                {
                    WindowsHelper.HandleError(e => 
                        macro.Messages.Error($"Simulate Key Error: [{e.NativeErrorCode}] {e.Message}"));
                } 
            }
        }
    }
}