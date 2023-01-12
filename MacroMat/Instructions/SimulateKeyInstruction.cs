using System.Runtime.InteropServices;

using MacroMat.Common;
using MacroMat.Input;

namespace MacroMat.Instructions;

public class SimulateKeyInstruction : MacroInstruction
{
    [DllImport("coredll.dll", SetLastError=true)]
    private static extern uint SendInput(uint cInputs, KEYBOARDINPUT[] inputs, int cbSize);
    
    private struct KEYBOARDINPUT
    {
        public uint type;
        public ushort wVk;
        ushort wScan;
        public uint dwFlags;
        uint time;
        uint dwExtraInfo;
        uint unused1;
        uint unused2;
    }
    
    public InputKey Key { get; }
    public KeyInputType Type { get; }

    public SimulateKeyInstruction(InputKey key, KeyInputType type)
    {
        Key = key;
        Type = type;
    }

    public override void Execute(Macro macro)
    {
        var loop = macro.Listener.MessageLoop;
        
        loop?.EnqueueAction(() =>
        {
            var os = new OS();

            os.OnWindows(() =>
            {
                SendInput();
            }).OnMac(() =>
            {

            }).OnLinux(() =>
            {
                
            }).Execute();
        });
    }
}