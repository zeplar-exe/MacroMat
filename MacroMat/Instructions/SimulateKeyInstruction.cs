using System.ComponentModel;
using System.Runtime.InteropServices;

using MacroMat.Common;
using MacroMat.Input;
using MacroMat.SystemCalls.Windows;

namespace MacroMat.Instructions;

public class SimulateKeyInstruction : MacroInstruction
{
    private uint? Scancode { get; }
    public InputKey Key { get; }
    public KeyInputType Type { get; }

    public SimulateKeyInstruction(uint scancode, KeyInputType type)
    {
        Scancode = scancode;
        Type = type;
    }
    
    public SimulateKeyInstruction(InputKey key, KeyInputType type)
    {
        Scancode = null;
        Key = key;
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
                var virtualKey = InputKeyTranslator.ToWindows(Key);

                if (virtualKey == null)
                {
                    macro.Messages.Log($"'{Key}' does not have a Windows virtual key equivalent.");
                    
                    return;
                }

                var flags = (Win32.KEYBDEVENTF)0;

                if (Scancode != null)
                    flags |= Win32.KEYBDEVENTF.SCANCODE;

                if (Type == KeyInputType.KeyUp)
                    flags |= Win32.KEYBDEVENTF.KEYUP;
                
                var scancode = Scancode ?? Win32.MapVirtualKeyEx(
                    (uint)virtualKey, 
                    (uint)Win32.MapVirtualKeyMapTypes.MAPVK_VK_TO_VSC, 
                    IntPtr.Zero);
                
                var input = new Win32.INPUT
                {
                    type = Win32.InputType.INPUT_KEYBOARD, 
                    U = new Win32.InputUnion
                    {
                        ki = new Win32.KEYBDINPUT
                        {
                            wVk = virtualKey.Value,
                            wScan = (Win32.WindowsScanCode)scancode,
                            time = 0,
                            dwFlags = flags
                        }
                    }
                };
                
                var items = new[] { input };
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