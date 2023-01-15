using System.Text;

using MacroMat.Common;
using MacroMat.Input;
using MacroMat.SystemCalls.Windows;

namespace MacroMat.Instructions;

public class SendUnicodeInstructions : MacroInstruction
{
    public string UnicodeString { get; }
    public KeyInputType Type { get; }

    public SendUnicodeInstructions(string unicodeString, KeyInputType type)
    {
        UnicodeString = unicodeString;
        Type = type;
    }

    public override void Execute(Macro macro)
    {
        var loop = macro.Listener.MessageLoop;
        
        loop?.EnqueueAction(() =>
        {
            var os = new OsSelector();

            os.OnWindows(() =>
            {
                var inputs = new List<Win32.INPUT>();
                var flags = Win32.KEYBDEVENTF.UNICODE;

                if (Type == KeyInputType.KeyUp)
                    flags |= Win32.KEYBDEVENTF.KEYUP;
                
                foreach (var code in UnicodeString.Select(Convert.ToInt32))
                {
                    var input = new Win32.INPUT
                    {
                        type = Win32.InputType.INPUT_KEYBOARD,
                        U = new Win32.InputUnion
                        {
                            ki = new Win32.KEYBDINPUT
                            {
                                wVk = 0,
                                wScan = (Win32.WindowsScanCode)code,
                                dwFlags = flags,
                                time = 0
                            }
                        }
                    };
                        
                    inputs.Add(input);
                }

                var items = inputs.ToArray();
                var result = Win32.SendInput((uint)items.Length, items, Win32.INPUT.Size);

                if (result != items.Length)
                {
                    WindowsHelper.HandleError(e => 
                        macro.Messages.Error($"Simulate Key Error: [{e.NativeErrorCode}] {e.Message}"));
                }
            }).Execute();
        });
    }
}