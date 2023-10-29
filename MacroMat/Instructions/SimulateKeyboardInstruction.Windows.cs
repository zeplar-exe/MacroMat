using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.UI.Input.KeyboardAndMouse;
using Windows.Win32.UI.TextServices;
using MacroMat.Common;
using MacroMat.Input;
using MacroMat.SystemCalls.Windows;

namespace MacroMat.Instructions;

public partial class SimulateKeyboardInstruction
{
    private void WindowsImplementation(Macro macro)
    {
        var flags = (KEYBD_EVENT_FLAGS)0;

        if (Data.Type == KeyInputType.KeyUp)
            flags |= KEYBD_EVENT_FLAGS.KEYEVENTF_KEYUP;

        var inputs = new INPUT[Data.Keys.Count];
        var index = 0;

        flags |= KEYBD_EVENT_FLAGS.KEYEVENTF_SCANCODE;
        
        foreach (var key in Data.Keys)
        {
            if (key is Scancode scancode)
            {
                var input = new INPUT
                {
                    type = INPUT_TYPE.INPUT_KEYBOARD,
                    Anonymous = new INPUT._Anonymous_e__Union
                    {
                        ki = new KEYBDINPUT
                        {
                            wScan = scancode.Value,
                            dwFlags = flags,
                            time = 0
                        }
                    }
                };
                
                inputs[index++] = input;
            }
            else if (key is VirtualKey virtualKey)
            {
                var virtualKeyScancode = PInvoke.MapVirtualKeyEx(
                    (uint)virtualKey.Value, 
                    (uint)MAP_VIRTUAL_KEY_TYPE.MAPVK_VK_TO_VSC, 
                    new HKL());
                
                var input = new INPUT
                {
                    type = INPUT_TYPE.INPUT_KEYBOARD,
                    Anonymous = new INPUT._Anonymous_e__Union
                    {
                        ki = new KEYBDINPUT
                        {
                            wVk = (VIRTUAL_KEY)virtualKey.Value,
                            wScan = (ushort)virtualKeyScancode,
                            dwFlags = flags,
                            time = 0
                        }
                    }
                };
                
                inputs[index++] = input;
            }
        }

        // Note to self:
        // if this every mysteriously stops working, make sure your enums are the correct numeric type
        // an int enum where there should be a byte messes things up

        unsafe
        {
            fixed (INPUT* inputPtr = &inputs[0])
            {
                var result = PInvoke.SendInput((uint)inputs.Length, inputPtr, Marshal.SizeOf<INPUT>());

                if (result != inputs.Length)
                {
                    WindowsHelper.HandleError(e => 
                        macro.Messages.Error($"Simulate Key Error: [{e.NativeErrorCode}] {e.Message}"));
                }
            }
        }
    }
}