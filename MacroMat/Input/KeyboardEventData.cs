using System.Text;

namespace MacroMat.Input;

/// <summary>
/// Low-level keyboard event data.
/// </summary>
public class KeyboardEventData
{
    /// <summary>
    /// Hardware-dependant scancode.
    /// </summary>
    public int HardwareScancode { get; }
    /// <summary>
    /// OS-dependant virtual code.
    /// </summary>
    public byte VirtualCode { get; }
    /// <summary>
    /// Input type of this keyboard event. 
    /// </summary>
    public KeyInputType Type { get; }
    /// <summary>
    /// Whether the event was simulated/injected.
    /// </summary>
    public bool IsInjected { get; }
    /// <summary>
    /// Whether the alt key is pressed for this keyboard event (counts for windows as sys keys, for example).
    /// </summary>
    public bool IsAlt { get; }

    internal KeyboardEventData(int hardwareScancode, byte virtualCode, KeyInputType type, bool isInjected, bool isAlt)
    {
        HardwareScancode = hardwareScancode;
        VirtualCode = virtualCode;
        Type = type;
        IsInjected = isInjected;
        IsAlt = isAlt;
    }

    /// <inheritdoc />
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