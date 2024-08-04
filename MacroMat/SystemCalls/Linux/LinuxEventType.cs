namespace MacroMat.SystemCalls.Linux;

public enum LinuxEventType
{
    /// <summary>
    /// Used as markers to separate events. Events may be separated in time or in space, such as with the multitouch protocol.
    /// </summary>
    EV_SYN = 0x00,

    /// <summary>
    /// Used to describe state changes of keyboards, buttons, or other key-like devices.
    /// </summary>
    EV_KEY = 0x01,

    /// <summary>
    /// Used to describe relative axis value changes, e.g. moving the mouse 5 units to the left.
    /// </summary>
    EV_REL = 0x02,

    /// <summary>
    /// Used to describe absolute axis value changes, e.g. describing the coordinates of a touch on a touchscreen.
    /// </summary>
    EV_ABS = 0x03,

    /// <summary>
    /// Used to describe miscellaneous input data that do not fit into other types.
    /// </summary>
    EV_MSC = 0x04,

    /// <summary>
    /// Used to describe binary state input switches.
    /// </summary>
    EV_SW = 0x05,

    /// <summary>
    /// Used to turn LEDs on devices on and off.
    /// </summary>
    EV_LED = 0x11,

    /// <summary>
    /// Used to output sound to devices.
    /// </summary>
    EV_SND = 0x12,

    /// <summary>
    /// Used for autorepeating devices.
    /// </summary>
    EV_REP = 0x014,

    /// <summary>
    /// Used to send force feedback commands to an input device.
    /// </summary>
    EV_FF = 0x15,

    /// <summary>
    /// A special type for power button and switch input.
    /// </summary>
    EV_PWR = 0x16,

    /// <summary>
    /// Used to receive force feedback device status.
    /// </summary>
    EV_FF_STATUS = 0x17,
}