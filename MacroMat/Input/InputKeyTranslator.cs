﻿using MacroMat.Common;
using MacroMat.SystemCalls.Windows;

namespace MacroMat.Input;

internal static class InputKeyTranslator
{
    public static Scancode CurrentPlatformScancode(InputKey key)
    {
        return new OsSelector<Scancode>()
            .OnWindows(() => ToWindowsScancode(key))
            .Execute();
    }
    
    public static VirtualKey? CurrentPlatformVirtual(InputKey key)
    {
        return new OsSelector<VirtualKey?>()
            .OnWindows(() =>
            {
                var windowsVirtualKey = ToWindowsVirtual(key);

                return windowsVirtualKey == null ? null : VirtualKey.From((byte)windowsVirtualKey.Value);
            })
            .Execute();
    }
    
    #region Windows
    
    public static Scancode ToWindowsScancode(InputKey key)
    {
        // https://docs.google.com/spreadsheets/d/1GSj0gKDxyWAecB3SIyEZ2ssPETZkkxn67gdIwL1zFUs/edit#gid=0
        
        return key switch
        {
            InputKey.F1 => Scancode.From(0x70),
            InputKey.F2 => Scancode.From(0x71),
            InputKey.F3 => Scancode.From(0x72),
            InputKey.F4 => Scancode.From(0x73),
            InputKey.F5 => Scancode.From(0x74),
            InputKey.F6 => Scancode.From(0x75),
            InputKey.F7 => Scancode.From(0x76),
            InputKey.F8 => Scancode.From(0x77),
            InputKey.F9 => Scancode.From(0x78),
            InputKey.F10 => Scancode.From(0x79),
            InputKey.F11 => Scancode.From(0x7A),
            InputKey.F12 => Scancode.From(0x7B),
            InputKey.F13 => Scancode.From(0x7C),
            InputKey.F14 => Scancode.From(0x7D),
            InputKey.F15 => Scancode.From(0x7E),
            InputKey.F16 => Scancode.From(0x7F),
            InputKey.F17 => Scancode.From(0x80),
            InputKey.F18 => Scancode.From(0x81),
            InputKey.F19 => Scancode.From(0x82),
            InputKey.F20 => Scancode.From(0x83),
            InputKey.F21 => Scancode.From(0x84),
            InputKey.F22 => Scancode.From(0x85),
            InputKey.F23 => Scancode.From(0x86),
            InputKey.F24 => Scancode.From(0x87),
            InputKey.VolumeMute => Scancode.From(0xAD),
            InputKey.VolumeDown => Scancode.From(0xAE),
            InputKey.VolumeUp => Scancode.From(0xAF),
            InputKey.PlaybackPrevious => Scancode.From(0xB1),
            InputKey.PlaybackPlayPause => Scancode.From(0xB3),
            InputKey.PlaybackNext => Scancode.From(0xB0),
            InputKey.Backtick => Scancode.From(0xC0),
            InputKey.Tilde => Scancode.From(0xC0),
            InputKey.Zero => Scancode.From(0x30),
            InputKey.One => Scancode.From(0x31),
            InputKey.Two => Scancode.From(0x32),
            InputKey.Three => Scancode.From(0x33),
            InputKey.Four => Scancode.From(0x34),
            InputKey.Five => Scancode.From(0x35),
            InputKey.Six => Scancode.From(0x36),
            InputKey.Seven => Scancode.From(0x37),
            InputKey.Eight => Scancode.From(0x38),
            InputKey.Nine => Scancode.From(0x39),
            InputKey.ExclamationPoint => Scancode.From(0x30),
            InputKey.At => Scancode.From(0x31),
            InputKey.Hashtag => Scancode.From(0x32),
            InputKey.Dollar => Scancode.From(0x33),
            InputKey.Percent => Scancode.From(0x34),
            InputKey.Caret => Scancode.From(0x35),
            InputKey.Ampersand => Scancode.From(0x36),
            InputKey.Star => Scancode.From(0x37),
            InputKey.LeftParenthesis => Scancode.From(0x38),
            InputKey.RightParenthesis => Scancode.From(0x39),
            InputKey.Dash => Scancode.From(0xBD),
            InputKey.Underscore => Scancode.From(0xBD),
            InputKey.Equals => Scancode.From(0xBB),
            InputKey.Plus => Scancode.From(0xBB),
            InputKey.LeftBracket => Scancode.From(0xDB),
            InputKey.LeftCurlyBracket => Scancode.From(0xDB),
            InputKey.RightBracket => Scancode.From(0xDD),
            InputKey.RightCurlyBracket => Scancode.From(0xDD),
            InputKey.VerticalBar => Scancode.From(0xDC),
            InputKey.Backslash => Scancode.From(0xDC),
            InputKey.Semicolon => Scancode.From(0xBA),
            InputKey.Colon => Scancode.From(0xBA),
            InputKey.SingleQuote => Scancode.From(0xDE),
            InputKey.DoubleQuote => Scancode.From(0xDE),
            InputKey.Comma => Scancode.From(0xBC),
            InputKey.LessThan => Scancode.From(0xBC),
            InputKey.Period => Scancode.From(0xBE),
            InputKey.GreaterThan => Scancode.From(0xBE),
            InputKey.Slash => Scancode.From(0xBF),
            InputKey.QuestionMark => Scancode.From(0xBF),
            InputKey.A => Scancode.From((short)Win32.WindowsScanCode.KEY_A), //Scancode.From(0x41),
            InputKey.B => Scancode.From(0x42),
            InputKey.C => Scancode.From(0x43),
            InputKey.D => Scancode.From(0x44),
            InputKey.E => Scancode.From(0x45),
            InputKey.F => Scancode.From(0x46),
            InputKey.G => Scancode.From(0x47),
            InputKey.H => Scancode.From(0x48),
            InputKey.I => Scancode.From(0x49),
            InputKey.J => Scancode.From(0x4A),
            InputKey.K => Scancode.From(0x4B),
            InputKey.L => Scancode.From(0x4C),
            InputKey.M => Scancode.From(0x4D),
            InputKey.N => Scancode.From(0x4E),
            InputKey.O => Scancode.From(0x4F),
            InputKey.P => Scancode.From(0x50),
            InputKey.Q => Scancode.From(0x51),
            InputKey.R => Scancode.From(0x52),
            InputKey.S => Scancode.From(0x53),
            InputKey.T => Scancode.From(0x54),
            InputKey.U => Scancode.From(0x55),
            InputKey.V => Scancode.From(0x56),
            InputKey.W => Scancode.From(0x57),
            InputKey.X => Scancode.From(0x58),
            InputKey.Y => Scancode.From(0x59),
            InputKey.Z => Scancode.From(0x5A),
            InputKey.Shift => Scancode.From(0x10),
            InputKey.LeftShift => Scancode.From(0xA0),
            InputKey.RightShift => Scancode.From(0xA1),
            InputKey.Alt => Scancode.From(0x12),
            InputKey.LeftAlt => Scancode.From(0xA4),
            InputKey.RightAlt => Scancode.From(0xA5),
            InputKey.Control => Scancode.From(0x11),
            InputKey.LeftControl => Scancode.From(0xA2),
            InputKey.RightControl => Scancode.From(0xA3),
            _ => throw new ArgumentOutOfRangeException(
                $"The InputKey {key} does not have a valid scancode equivalent on Windows.")
        };
    }

    public static Win32.WindowsVirtualKey? ToWindowsVirtual(InputKey key)
    {
        // https://learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
        
        return key switch
        {
            InputKey.F1 => Win32.WindowsVirtualKey.F1,
            InputKey.F2 => Win32.WindowsVirtualKey.F2,
            InputKey.F3 => Win32.WindowsVirtualKey.F3,
            InputKey.F4 => Win32.WindowsVirtualKey.F4,
            InputKey.F5 => Win32.WindowsVirtualKey.F5,
            InputKey.F6 => Win32.WindowsVirtualKey.F6,
            InputKey.F7 => Win32.WindowsVirtualKey.F7,
            InputKey.F8 => Win32.WindowsVirtualKey.F8,
            InputKey.F9 => Win32.WindowsVirtualKey.F9,
            InputKey.F10 => Win32.WindowsVirtualKey.F10,
            InputKey.F11 => Win32.WindowsVirtualKey.F11,
            InputKey.F12 => Win32.WindowsVirtualKey.F12,
            InputKey.F13 => Win32.WindowsVirtualKey.F13,
            InputKey.F14 => Win32.WindowsVirtualKey.F14,
            InputKey.F15 => Win32.WindowsVirtualKey.F15,
            InputKey.F16 => Win32.WindowsVirtualKey.F16,
            InputKey.F17 => Win32.WindowsVirtualKey.F17,
            InputKey.F18 => Win32.WindowsVirtualKey.F18,
            InputKey.F19 => Win32.WindowsVirtualKey.F19,
            InputKey.F20 => Win32.WindowsVirtualKey.F20,
            InputKey.F21 => Win32.WindowsVirtualKey.F21,
            InputKey.F22 => Win32.WindowsVirtualKey.F22,
            InputKey.F23 => Win32.WindowsVirtualKey.F23,
            InputKey.F24 => Win32.WindowsVirtualKey.F24,
            InputKey.VolumeMute => Win32.WindowsVirtualKey.VOLUME_MUTE,
            InputKey.VolumeDown => Win32.WindowsVirtualKey.VOLUME_UP,
            InputKey.VolumeUp => Win32.WindowsVirtualKey.VOLUME_DOWN,
            InputKey.PlaybackPrevious => Win32.WindowsVirtualKey.MEDIA_PREV_TRACK,
            InputKey.PlaybackPlayPause => Win32.WindowsVirtualKey.MEDIA_PLAY_PAUSE,
            InputKey.PlaybackNext => Win32.WindowsVirtualKey.MEDIA_NEXT_TRACK,
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
            InputKey.Shift => Win32.WindowsVirtualKey.SHIFT,
            InputKey.LeftShift => Win32.WindowsVirtualKey.LSHIFT,
            InputKey.RightShift => Win32.WindowsVirtualKey.RSHIFT,
            InputKey.Alt => Win32.WindowsVirtualKey.MENU,
            InputKey.LeftAlt => Win32.WindowsVirtualKey.LMENU,
            InputKey.RightAlt => Win32.WindowsVirtualKey.RMENU,
            InputKey.Control => Win32.WindowsVirtualKey.CONTROL,
            InputKey.LeftControl => Win32.WindowsVirtualKey.LCONTROL,
            InputKey.RightControl => Win32.WindowsVirtualKey.RCONTROL,
            InputKey.Backtick => Win32.WindowsVirtualKey.OEM_3,
            InputKey.Tilde => Win32.WindowsVirtualKey.OEM_3,
            InputKey.ExclamationPoint => Win32.WindowsVirtualKey.KEY_1,
            InputKey.At => Win32.WindowsVirtualKey.KEY_2,
            InputKey.Hashtag => Win32.WindowsVirtualKey.KEY_3,
            InputKey.Dollar => Win32.WindowsVirtualKey.KEY_4,
            InputKey.Percent => Win32.WindowsVirtualKey.KEY_5,
            InputKey.Caret => Win32.WindowsVirtualKey.KEY_6,
            InputKey.Ampersand => Win32.WindowsVirtualKey.KEY_7,
            InputKey.Star => Win32.WindowsVirtualKey.MULTIPLY,
            InputKey.LeftParenthesis => Win32.WindowsVirtualKey.KEY_9,
            InputKey.RightParenthesis => Win32.WindowsVirtualKey.KEY_0,
            InputKey.Dash => Win32.WindowsVirtualKey.SUBTRACT,
            InputKey.Underscore => Win32.WindowsVirtualKey.OEM_MINUS,
            InputKey.Equals => Win32.WindowsVirtualKey.OEM_PLUS,
            InputKey.Plus => Win32.WindowsVirtualKey.ADD,
            InputKey.LeftBracket => Win32.WindowsVirtualKey.OEM_4,
            InputKey.LeftCurlyBracket => (Win32.WindowsVirtualKey)123, //Win32.WindowsVirtualKey.OEM_4,
            InputKey.RightBracket => Win32.WindowsVirtualKey.OEM_6,
            InputKey.RightCurlyBracket => Win32.WindowsVirtualKey.OEM_6,
            InputKey.VerticalBar => Win32.WindowsVirtualKey.OEM_5,
            InputKey.Backslash => Win32.WindowsVirtualKey.OEM_5,
            InputKey.Semicolon => Win32.WindowsVirtualKey.OEM_1,
            InputKey.Colon => Win32.WindowsVirtualKey.OEM_1,
            InputKey.SingleQuote => Win32.WindowsVirtualKey.OEM_7,
            InputKey.DoubleQuote => Win32.WindowsVirtualKey.OEM_7,
            InputKey.Comma => Win32.WindowsVirtualKey.OEM_COMMA,
            InputKey.LessThan => Win32.WindowsVirtualKey.OEM_COMMA,
            InputKey.Period => Win32.WindowsVirtualKey.OEM_PERIOD,
            InputKey.GreaterThan => Win32.WindowsVirtualKey.OEM_PERIOD,
            InputKey.Slash => Win32.WindowsVirtualKey.DIVIDE,
            InputKey.QuestionMark => Win32.WindowsVirtualKey.OEM_2,
            _ => throw new ArgumentOutOfRangeException(nameof(key), key, null)
        };
    }
    
    #endregion
}