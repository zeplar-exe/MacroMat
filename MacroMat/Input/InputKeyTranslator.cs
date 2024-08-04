using Windows.Win32.UI.Input.KeyboardAndMouse;
using MacroMat.Common;

namespace MacroMat.Input;

internal static class InputKeyTranslator
{
    public static VirtualKey CurrentPlatformVirtual(InputKey key)
    {
        return new OsSelector<VirtualKey>()
            .OnWindows(() =>
            {
                var windowsVirtualKey = ToWindowsVirtual(key);

                return VirtualKey.From((short)windowsVirtualKey);
            })
            .Execute();
    }

    #region Windows
    
    public static VIRTUAL_KEY ToWindowsVirtual(InputKey key)
    {
        // https://learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
        
        return key switch
        {
            InputKey.F1 => VIRTUAL_KEY.VK_F1,
            InputKey.F2 => VIRTUAL_KEY.VK_F2,
            InputKey.F3 => VIRTUAL_KEY.VK_F3,
            InputKey.F4 => VIRTUAL_KEY.VK_F4,
            InputKey.F5 => VIRTUAL_KEY.VK_F5,
            InputKey.F6 => VIRTUAL_KEY.VK_F6,
            InputKey.F7 => VIRTUAL_KEY.VK_F7,
            InputKey.F8 => VIRTUAL_KEY.VK_F8,
            InputKey.F9 => VIRTUAL_KEY.VK_F9,
            InputKey.F10 => VIRTUAL_KEY.VK_F10,
            InputKey.F11 => VIRTUAL_KEY.VK_F11,
            InputKey.F12 => VIRTUAL_KEY.VK_F12,
            InputKey.F13 => VIRTUAL_KEY.VK_F13,
            InputKey.F14 => VIRTUAL_KEY.VK_F14,
            InputKey.F15 => VIRTUAL_KEY.VK_F15,
            InputKey.F16 => VIRTUAL_KEY.VK_F16,
            InputKey.F17 => VIRTUAL_KEY.VK_F17,
            InputKey.F18 => VIRTUAL_KEY.VK_F18,
            InputKey.F19 => VIRTUAL_KEY.VK_F19,
            InputKey.F20 => VIRTUAL_KEY.VK_F20,
            InputKey.F21 => VIRTUAL_KEY.VK_F21,
            InputKey.F22 => VIRTUAL_KEY.VK_F22,
            InputKey.F23 => VIRTUAL_KEY.VK_F23,
            InputKey.F24 => VIRTUAL_KEY.VK_F24,
            InputKey.VolumeMute => VIRTUAL_KEY.VK_VOLUME_MUTE,
            InputKey.VolumeDown => VIRTUAL_KEY.VK_VOLUME_UP,
            InputKey.VolumeUp => VIRTUAL_KEY.VK_VOLUME_DOWN,
            InputKey.PlaybackPrevious => VIRTUAL_KEY.VK_MEDIA_PREV_TRACK,
            InputKey.PlaybackPlayPause => VIRTUAL_KEY.VK_MEDIA_PLAY_PAUSE,
            InputKey.PlaybackNext => VIRTUAL_KEY.VK_MEDIA_NEXT_TRACK,
            InputKey.Zero => VIRTUAL_KEY.VK_0,
            InputKey.One => VIRTUAL_KEY.VK_1,
            InputKey.Two => VIRTUAL_KEY.VK_2,
            InputKey.Three => VIRTUAL_KEY.VK_3,
            InputKey.Four => VIRTUAL_KEY.VK_4,
            InputKey.Five => VIRTUAL_KEY.VK_5,
            InputKey.Six => VIRTUAL_KEY.VK_6,
            InputKey.Seven => VIRTUAL_KEY.VK_7,
            InputKey.Eight => VIRTUAL_KEY.VK_8,
            InputKey.Nine => VIRTUAL_KEY.VK_9,
            InputKey.A => VIRTUAL_KEY.VK_A,
            InputKey.B => VIRTUAL_KEY.VK_B,
            InputKey.C => VIRTUAL_KEY.VK_C,
            InputKey.D => VIRTUAL_KEY.VK_D,
            InputKey.E => VIRTUAL_KEY.VK_E,
            InputKey.F => VIRTUAL_KEY.VK_F,
            InputKey.G => VIRTUAL_KEY.VK_G,
            InputKey.H => VIRTUAL_KEY.VK_H,
            InputKey.I => VIRTUAL_KEY.VK_I,
            InputKey.J => VIRTUAL_KEY.VK_J,
            InputKey.K => VIRTUAL_KEY.VK_K,
            InputKey.L => VIRTUAL_KEY.VK_L,
            InputKey.M => VIRTUAL_KEY.VK_M,
            InputKey.N => VIRTUAL_KEY.VK_N,
            InputKey.O => VIRTUAL_KEY.VK_O,
            InputKey.P => VIRTUAL_KEY.VK_P,
            InputKey.Q => VIRTUAL_KEY.VK_Q,
            InputKey.R => VIRTUAL_KEY.VK_R,
            InputKey.S => VIRTUAL_KEY.VK_S,
            InputKey.T => VIRTUAL_KEY.VK_T,
            InputKey.U => VIRTUAL_KEY.VK_U,
            InputKey.V => VIRTUAL_KEY.VK_V,
            InputKey.W => VIRTUAL_KEY.VK_W,
            InputKey.X => VIRTUAL_KEY.VK_X,
            InputKey.Y => VIRTUAL_KEY.VK_Y,
            InputKey.Z => VIRTUAL_KEY.VK_Z,
            InputKey.WindowsKey => VIRTUAL_KEY.VK_LWIN,
            InputKey.LeftWindowsKey => VIRTUAL_KEY.VK_LWIN,
            InputKey.RightWindowsKey => VIRTUAL_KEY.VK_RWIN,
            InputKey.Shift => VIRTUAL_KEY.VK_SHIFT,
            InputKey.LeftShift => VIRTUAL_KEY.VK_LSHIFT,
            InputKey.RightShift => VIRTUAL_KEY.VK_RSHIFT,
            InputKey.Alt => VIRTUAL_KEY.VK_MENU,
            InputKey.LeftAlt => VIRTUAL_KEY.VK_LMENU,
            InputKey.RightAlt => VIRTUAL_KEY.VK_RMENU,
            InputKey.Control => VIRTUAL_KEY.VK_CONTROL,
            InputKey.LeftControl => VIRTUAL_KEY.VK_LCONTROL,
            InputKey.RightControl => VIRTUAL_KEY.VK_RCONTROL,
            InputKey.Backtick => VIRTUAL_KEY.VK_OEM_3,
            InputKey.Dash => VIRTUAL_KEY.VK_SUBTRACT,
            InputKey.Underscore => VIRTUAL_KEY.VK_OEM_MINUS,
            InputKey.Equals => VIRTUAL_KEY.VK_OEM_PLUS,
            InputKey.Plus => VIRTUAL_KEY.VK_ADD,
            InputKey.LeftBracket => VIRTUAL_KEY.VK_OEM_4,
            InputKey.RightBracket => VIRTUAL_KEY.VK_OEM_6,
            InputKey.VerticalBar => VIRTUAL_KEY.VK_OEM_5,
            InputKey.Backslash => VIRTUAL_KEY.VK_OEM_5,
            InputKey.Semicolon => VIRTUAL_KEY.VK_OEM_1,
            InputKey.Colon => VIRTUAL_KEY.VK_OEM_1,
            InputKey.SingleQuote => VIRTUAL_KEY.VK_OEM_7,
            InputKey.Comma => VIRTUAL_KEY.VK_OEM_COMMA,
            InputKey.Period => VIRTUAL_KEY.VK_OEM_PERIOD,
            InputKey.Slash => VIRTUAL_KEY.VK_DIVIDE,
            InputKey.Enter => VIRTUAL_KEY.VK_RETURN,
            InputKey.Tab => VIRTUAL_KEY.VK_TAB,
            InputKey.CapsLock => VIRTUAL_KEY.VK_CAPITAL,
            InputKey.Numlock => VIRTUAL_KEY.VK_NUMLOCK,
            InputKey.Escape => VIRTUAL_KEY.VK_ESCAPE,
            InputKey.Delete => VIRTUAL_KEY.VK_DELETE,
            InputKey.Backspace => VIRTUAL_KEY.VK_BACK,
            InputKey.Num0 => VIRTUAL_KEY.VK_NUMPAD0,
            InputKey.Num1 => VIRTUAL_KEY.VK_NUMPAD1,
            InputKey.Num2 => VIRTUAL_KEY.VK_NUMPAD2,
            InputKey.Num3 => VIRTUAL_KEY.VK_NUMPAD3,
            InputKey.Num4 => VIRTUAL_KEY.VK_NUMPAD4,
            InputKey.Num5 => VIRTUAL_KEY.VK_NUMPAD5,
            InputKey.Num6 => VIRTUAL_KEY.VK_NUMPAD6,
            InputKey.Num7 => VIRTUAL_KEY.VK_NUMPAD7,
            InputKey.Num8 => VIRTUAL_KEY.VK_NUMPAD8,
            InputKey.Num9 => VIRTUAL_KEY.VK_NUMPAD9,
            InputKey.LeftArrow => VIRTUAL_KEY.VK_LEFT,
            InputKey.RightArrow => VIRTUAL_KEY.VK_RIGHT,
            InputKey.UpArrow => VIRTUAL_KEY.VK_UP,
            InputKey.DownArrow => VIRTUAL_KEY.VK_DOWN,
            InputKey.Home => VIRTUAL_KEY.VK_HOME,
            InputKey.End => VIRTUAL_KEY.VK_END,
            InputKey.PageUp => VIRTUAL_KEY.VK_PRIOR,
            InputKey.PageDown => VIRTUAL_KEY.VK_NEXT,
            InputKey.Insert => VIRTUAL_KEY.VK_INSERT,
            InputKey.PrintScreen => VIRTUAL_KEY.VK_PRINT,
            _ => throw new ArgumentOutOfRangeException(
                $"{key} does not have a virtual key equivalent on Windows.")
        };
    }
    
    #endregion
}