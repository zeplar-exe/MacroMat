﻿using Windows.Win32.UI.Input.KeyboardAndMouse;
using MacroMat.Common;
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
    
    public static Scancode ToWindowsScancode(InputKey key)
    {
        return key switch
        {
            InputKey.F1 => Scancode.From((ushort)Win32.WindowsScanCode.F1),
            InputKey.F2 => Scancode.From((ushort)Win32.WindowsScanCode.F2),
            InputKey.F3 => Scancode.From((ushort)Win32.WindowsScanCode.F3),
            InputKey.F4 => Scancode.From((ushort)Win32.WindowsScanCode.F4),
            InputKey.F5 => Scancode.From((ushort)Win32.WindowsScanCode.F5),
            InputKey.F6 => Scancode.From((ushort)Win32.WindowsScanCode.F6),
            InputKey.F7 => Scancode.From((ushort)Win32.WindowsScanCode.F7),
            InputKey.F8 => Scancode.From((ushort)Win32.WindowsScanCode.F8),
            InputKey.F9 => Scancode.From((ushort)Win32.WindowsScanCode.F9),
            InputKey.F10 => Scancode.From((ushort)Win32.WindowsScanCode.F10),
            InputKey.F11 => Scancode.From((ushort)Win32.WindowsScanCode.F11),
            InputKey.F12 => Scancode.From((ushort)Win32.WindowsScanCode.F12),
            InputKey.F13 => Scancode.From((ushort)Win32.WindowsScanCode.F13),
            InputKey.F14 => Scancode.From((ushort)Win32.WindowsScanCode.F14),
            InputKey.F15 => Scancode.From((ushort)Win32.WindowsScanCode.F15),
            InputKey.F16 => Scancode.From((ushort)Win32.WindowsScanCode.F16),
            InputKey.F17 => Scancode.From((ushort)Win32.WindowsScanCode.F17),
            InputKey.F18 => Scancode.From((ushort)Win32.WindowsScanCode.F18),
            InputKey.F19 => Scancode.From((ushort)Win32.WindowsScanCode.F19),
            InputKey.F20 => Scancode.From((ushort)Win32.WindowsScanCode.F20),
            InputKey.F21 => Scancode.From((ushort)Win32.WindowsScanCode.F21),
            InputKey.F22 => Scancode.From((ushort)Win32.WindowsScanCode.F22),
            InputKey.F23 => Scancode.From((ushort)Win32.WindowsScanCode.F23),
            InputKey.F24 => Scancode.From((ushort)Win32.WindowsScanCode.F24),
            InputKey.VolumeMute => Scancode.From((ushort)Win32.WindowsScanCode.VOLUME_MUTE),
            InputKey.VolumeDown => Scancode.From((ushort)Win32.WindowsScanCode.VOLUME_DOWN),
            InputKey.VolumeUp => Scancode.From((ushort)Win32.WindowsScanCode.VOLUME_UP),
            InputKey.PlaybackPrevious => Scancode.From((ushort)Win32.WindowsScanCode.MEDIA_PREV_TRACK),
            InputKey.PlaybackPlayPause => Scancode.From((ushort)Win32.WindowsScanCode.MEDIA_PLAY_PAUSE),
            InputKey.PlaybackNext => Scancode.From((ushort)Win32.WindowsScanCode.MEDIA_NEXT_TRACK),
            InputKey.Backtick => Scancode.From(0xC0),
            InputKey.Tilde => Scancode.From(0xC0),
            InputKey.Zero => Scancode.From((ushort)Win32.WindowsScanCode.KEY_0),
            InputKey.One => Scancode.From((ushort)Win32.WindowsScanCode.KEY_1),
            InputKey.Two => Scancode.From((ushort)Win32.WindowsScanCode.KEY_2),
            InputKey.Three => Scancode.From((ushort)Win32.WindowsScanCode.KEY_3),
            InputKey.Four => Scancode.From((ushort)Win32.WindowsScanCode.KEY_4),
            InputKey.Five => Scancode.From((ushort)Win32.WindowsScanCode.KEY_5),
            InputKey.Six => Scancode.From((ushort)Win32.WindowsScanCode.KEY_6),
            InputKey.Seven => Scancode.From((ushort)Win32.WindowsScanCode.KEY_7),
            InputKey.Eight => Scancode.From((ushort)Win32.WindowsScanCode.KEY_8),
            InputKey.Nine => Scancode.From((ushort)Win32.WindowsScanCode.KEY_9),
            InputKey.ExclamationPoint => Scancode.From((ushort)Win32.WindowsScanCode.KEY_1),
            InputKey.At => Scancode.From((ushort)Win32.WindowsScanCode.KEY_2),
            InputKey.Hashtag => Scancode.From((ushort)Win32.WindowsScanCode.KEY_3),
            InputKey.Dollar => Scancode.From((ushort)Win32.WindowsScanCode.KEY_4),
            InputKey.Percent => Scancode.From((ushort)Win32.WindowsScanCode.KEY_5),
            InputKey.Caret => Scancode.From((ushort)Win32.WindowsScanCode.KEY_6),
            InputKey.Ampersand => Scancode.From((ushort)Win32.WindowsScanCode.KEY_7),
            InputKey.Star => Scancode.From((ushort)Win32.WindowsScanCode.KEY_8),
            InputKey.LeftParenthesis => Scancode.From((ushort)Win32.WindowsScanCode.KEY_9),
            InputKey.RightParenthesis => Scancode.From((ushort)Win32.WindowsScanCode.KEY_0),
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
            InputKey.A => Scancode.From((ushort)Win32.WindowsScanCode.KEY_A),
            InputKey.B => Scancode.From((ushort)Win32.WindowsScanCode.KEY_B),
            InputKey.C => Scancode.From((ushort)Win32.WindowsScanCode.KEY_C),
            InputKey.D => Scancode.From((ushort)Win32.WindowsScanCode.KEY_D),
            InputKey.E => Scancode.From((ushort)Win32.WindowsScanCode.KEY_E),
            InputKey.F => Scancode.From((ushort)Win32.WindowsScanCode.KEY_F),
            InputKey.G => Scancode.From((ushort)Win32.WindowsScanCode.KEY_G),
            InputKey.H => Scancode.From((ushort)Win32.WindowsScanCode.KEY_H),
            InputKey.I => Scancode.From((ushort)Win32.WindowsScanCode.KEY_I),
            InputKey.J => Scancode.From((ushort)Win32.WindowsScanCode.KEY_J),
            InputKey.K => Scancode.From((ushort)Win32.WindowsScanCode.KEY_K),
            InputKey.L => Scancode.From((ushort)Win32.WindowsScanCode.KEY_L),
            InputKey.M => Scancode.From((ushort)Win32.WindowsScanCode.KEY_M),
            InputKey.N => Scancode.From((ushort)Win32.WindowsScanCode.KEY_N),
            InputKey.O => Scancode.From((ushort)Win32.WindowsScanCode.KEY_O),
            InputKey.P => Scancode.From((ushort)Win32.WindowsScanCode.KEY_P),
            InputKey.Q => Scancode.From((ushort)Win32.WindowsScanCode.KEY_Q),
            InputKey.R => Scancode.From((ushort)Win32.WindowsScanCode.KEY_R),
            InputKey.S => Scancode.From((ushort)Win32.WindowsScanCode.KEY_S),
            InputKey.T => Scancode.From((ushort)Win32.WindowsScanCode.KEY_T),
            InputKey.U => Scancode.From((ushort)Win32.WindowsScanCode.KEY_U),
            InputKey.V => Scancode.From((ushort)Win32.WindowsScanCode.KEY_V),
            InputKey.W => Scancode.From((ushort)Win32.WindowsScanCode.KEY_W),
            InputKey.X => Scancode.From((ushort)Win32.WindowsScanCode.KEY_X),
            InputKey.Y => Scancode.From((ushort)Win32.WindowsScanCode.KEY_Y),
            InputKey.Z => Scancode.From((ushort)Win32.WindowsScanCode.KEY_Z),
            InputKey.Shift => Scancode.From((ushort)Win32.WindowsScanCode.SHIFT),
            InputKey.LeftShift => Scancode.From((ushort)Win32.WindowsScanCode.LSHIFT),
            InputKey.RightShift => Scancode.From((ushort)Win32.WindowsScanCode.RSHIFT),
            InputKey.Alt => Scancode.From(0x12),
            InputKey.LeftAlt => Scancode.From(0xA4),
            InputKey.RightAlt => Scancode.From(0xA5),
            InputKey.Control => Scancode.From((ushort)Win32.WindowsScanCode.CONTROL),
            InputKey.LeftControl => Scancode.From((ushort)Win32.WindowsScanCode.LCONTROL),
            InputKey.RightControl => Scancode.From((ushort)Win32.WindowsScanCode.RCONTROL),
            InputKey.Enter => Scancode.From((ushort)Win32.WindowsScanCode.RETURN),
            InputKey.Tab => Scancode.From((ushort)Win32.WindowsScanCode.TAB),
            InputKey.CapsLock => Scancode.From((ushort)Win32.WindowsScanCode.CAPITAL),
            InputKey.Numlock => Scancode.From((ushort)Win32.WindowsScanCode.NUMLOCK),
            InputKey.Delete => Scancode.From((ushort)Win32.WindowsScanCode.DELETE),
            InputKey.Backspace => Scancode.From((ushort)Win32.WindowsScanCode.BACK),
            InputKey.Num0 => Scancode.From((ushort)Win32.WindowsScanCode.NUMPAD0),
            InputKey.Num1 => Scancode.From((ushort)Win32.WindowsScanCode.NUMPAD1),
            InputKey.Num2 => Scancode.From((ushort)Win32.WindowsScanCode.NUMPAD2),
            InputKey.Num3 => Scancode.From((ushort)Win32.WindowsScanCode.NUMPAD3),
            InputKey.Num4 => Scancode.From((ushort)Win32.WindowsScanCode.NUMPAD4),
            InputKey.Num5 => Scancode.From((ushort)Win32.WindowsScanCode.NUMPAD5),
            InputKey.Num6 => Scancode.From((ushort)Win32.WindowsScanCode.NUMPAD6),
            InputKey.Num7 => Scancode.From((ushort)Win32.WindowsScanCode.NUMPAD7),
            InputKey.Num8 => Scancode.From((ushort)Win32.WindowsScanCode.NUMPAD8),
            InputKey.Num9 => Scancode.From((ushort)Win32.WindowsScanCode.NUMPAD9),
            InputKey.LeftArrow => Scancode.From((ushort)Win32.WindowsScanCode.LEFT),
            InputKey.RightArrow => Scancode.From((ushort)Win32.WindowsScanCode.RIGHT),
            InputKey.UpArrow => Scancode.From((ushort)Win32.WindowsScanCode.UP),
            InputKey.DownArrow => Scancode.From((ushort)Win32.WindowsScanCode.DOWN),
            _ => throw new ArgumentOutOfRangeException(
                $"{key} does not have a valid scancode equivalent on Windows.")
        };
    }

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
            InputKey.Tilde => VIRTUAL_KEY.VK_OEM_3,
            InputKey.ExclamationPoint => VIRTUAL_KEY.VK_1,
            InputKey.At => VIRTUAL_KEY.VK_2,
            InputKey.Hashtag => VIRTUAL_KEY.VK_3,
            InputKey.Dollar => VIRTUAL_KEY.VK_4,
            InputKey.Percent => VIRTUAL_KEY.VK_5,
            InputKey.Caret => VIRTUAL_KEY.VK_6,
            InputKey.Ampersand => VIRTUAL_KEY.VK_7,
            InputKey.Star => VIRTUAL_KEY.VK_MULTIPLY,
            InputKey.LeftParenthesis => VIRTUAL_KEY.VK_9,
            InputKey.RightParenthesis => VIRTUAL_KEY.VK_0,
            InputKey.Dash => VIRTUAL_KEY.VK_SUBTRACT,
            InputKey.Underscore => VIRTUAL_KEY.VK_OEM_MINUS,
            InputKey.Equals => VIRTUAL_KEY.VK_OEM_PLUS,
            InputKey.Plus => VIRTUAL_KEY.VK_ADD,
            InputKey.LeftBracket => VIRTUAL_KEY.VK_OEM_4,
            InputKey.LeftCurlyBracket => VIRTUAL_KEY.VK_OEM_4,
            InputKey.RightBracket => VIRTUAL_KEY.VK_OEM_6,
            InputKey.RightCurlyBracket => VIRTUAL_KEY.VK_OEM_6,
            InputKey.VerticalBar => VIRTUAL_KEY.VK_OEM_5,
            InputKey.Backslash => VIRTUAL_KEY.VK_OEM_5,
            InputKey.Semicolon => VIRTUAL_KEY.VK_OEM_1,
            InputKey.Colon => VIRTUAL_KEY.VK_OEM_1,
            InputKey.SingleQuote => VIRTUAL_KEY.VK_OEM_7,
            InputKey.DoubleQuote => VIRTUAL_KEY.VK_OEM_7,
            InputKey.Comma => VIRTUAL_KEY.VK_OEM_COMMA,
            InputKey.LessThan => VIRTUAL_KEY.VK_OEM_COMMA,
            InputKey.Period => VIRTUAL_KEY.VK_OEM_PERIOD,
            InputKey.GreaterThan => VIRTUAL_KEY.VK_OEM_PERIOD,
            InputKey.Slash => VIRTUAL_KEY.VK_DIVIDE,
            InputKey.QuestionMark => VIRTUAL_KEY.VK_OEM_2,
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
            _ => throw new ArgumentOutOfRangeException(
                $"{key} does not have a valid virtual key equivalent on Windows.")
        };
    }
    
    #endregion
}