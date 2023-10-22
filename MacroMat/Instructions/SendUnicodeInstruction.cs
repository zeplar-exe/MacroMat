using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.UI.Input.KeyboardAndMouse;
using MacroMat.Common;
using MacroMat.Input;
using MacroMat.SystemCalls.Windows;

namespace MacroMat.Instructions;

/// <summary>
/// Instruction to send unicode input, best used for typing text.
/// </summary>
public class SendUnicodeInstruction : MacroInstruction
{
    /// <summary>
    /// String of unicode characters to simulate.
    /// </summary>
    public string UnicodeString { get; }
    /// <summary>
    /// Input type for the unicode string.
    /// </summary>
    public KeyInputType Type { get; }

    /// <inheritdoc />
    public SendUnicodeInstruction(string unicodeString, KeyInputType type)
    {
        UnicodeString = unicodeString;
        Type = type;
    }

    /// <inheritdoc />
    public override void Execute(Macro macro)
    {
        var loop = macro.Listener.MessageLoop;
        
        loop?.EnqueueAction(() =>
        {
            var os = new OsSelector();

            os.OnWindows(() =>
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
            }).Execute();
        });
    }
}