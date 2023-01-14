using MacroMat.SystemCalls.Windows;

namespace MacroMat.Input;

internal static class InputKeyTranslator
{
    public static Win32.WindowsVirtualKey? ToWindows(InputKey key)
    {
        return key switch
        {
            InputKey.Zero => Win32.WindowsVirtualKey.KEY_0,
            InputKey.One => Win32.WindowsVirtualKey.KEY_1,
            InputKey.Two => Win32.WindowsVirtualKey.KEY_2,
            InputKey.Three => Win32.WindowsVirtualKey.KEY_3,
            InputKey.Four => Win32.WindowsVirtualKey.KEY_4,
            InputKey.Five => Win32.WindowsVirtualKey.KEY_5,
            InputKey.Six => Win32.WindowsVirtualKey.KEY_6,
            InputKey.Seven => Win32.WindowsVirtualKey.KEY_7,
            InputKey.Eight => Win32.WindowsVirtualKey.KEY_8,
            InputKey.Nine => Win32.WindowsVirtualKey.KEY_9,
            InputKey.A => Win32.WindowsVirtualKey.KEY_A,
            InputKey.B => Win32.WindowsVirtualKey.KEY_B,
            InputKey.C => Win32.WindowsVirtualKey.KEY_C,
            InputKey.D => Win32.WindowsVirtualKey.KEY_D,
            InputKey.E => Win32.WindowsVirtualKey.KEY_E,
            InputKey.F => Win32.WindowsVirtualKey.KEY_F,
            InputKey.G => Win32.WindowsVirtualKey.KEY_G,
            InputKey.H => Win32.WindowsVirtualKey.KEY_H,
            InputKey.I => Win32.WindowsVirtualKey.KEY_I,
            InputKey.J => Win32.WindowsVirtualKey.KEY_J,
            InputKey.K => Win32.WindowsVirtualKey.KEY_K,
            InputKey.L => Win32.WindowsVirtualKey.KEY_L,
            InputKey.M => Win32.WindowsVirtualKey.KEY_M,
            InputKey.N => Win32.WindowsVirtualKey.KEY_N,
            InputKey.O => Win32.WindowsVirtualKey.KEY_O,
            InputKey.P => Win32.WindowsVirtualKey.KEY_P,
            InputKey.Q => Win32.WindowsVirtualKey.KEY_Q,
            InputKey.R => Win32.WindowsVirtualKey.KEY_R,
            InputKey.S => Win32.WindowsVirtualKey.KEY_S,
            InputKey.T => Win32.WindowsVirtualKey.KEY_T,
            InputKey.U => Win32.WindowsVirtualKey.KEY_U,
            InputKey.V => Win32.WindowsVirtualKey.KEY_V,
            InputKey.W => Win32.WindowsVirtualKey.KEY_W,
            InputKey.X => Win32.WindowsVirtualKey.KEY_X,
            InputKey.Y => Win32.WindowsVirtualKey.KEY_Y,
            InputKey.Z => Win32.WindowsVirtualKey.KEY_Z,
            _ => null
        };
    }
}