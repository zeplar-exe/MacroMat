using System.Text;

using MacroMat.Input;

namespace MacroMat.SystemCalls;

public class KeyboardEventData
{
    public int HardwareScancode { get; }
    public int VirtualCode { get; }
    public KeyInputType Type { get; }
    public bool IsInjected { get; }

    public KeyboardEventData(int hardwareScancode, int virtualCode, KeyInputType type, bool isInjected)
    {
        HardwareScancode = hardwareScancode;
        VirtualCode = virtualCode;
        Type = type;
        IsInjected = isInjected;
    }

    public override string ToString()
    {
        return new StringBuilder()
            .Append("{ ")
            .Append("Hardware Code: ").Append(HardwareScancode).Append(", ")
            .Append("Virtual Code: ").Append(VirtualCode).Append(", ")
            .Append("Type: ").Append(Type).Append(", ")
            .Append("Is Injection: ").Append(IsInjected)
            .Append(" }")
            .ToString();
    }
}