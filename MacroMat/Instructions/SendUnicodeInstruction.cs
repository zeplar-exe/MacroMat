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
public partial class SendUnicodeInstruction : MacroInstruction
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
                WindowsImplementation(macro);
            }).Execute();
        });
    }
}