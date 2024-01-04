using System.Diagnostics;
using MacroMat.Common;
using MacroMat.Input;

namespace MacroMat.SystemCalls.Linux;

// Much thanks to this man on SO: https://stackoverflow.com/questions/69414090

internal class LinuxHook : IPlatformHook, IKeyboardHook, IMouseHook
{
    public event KeyboardHookCallback? OnKeyEvent;
    public event MouseHookCallback? OnMouseEvent;
    
    public bool Init(MessageLoop messageLoop)
    {
        var linuxMessageLoop = (LinuxMessageLoop)messageLoop;
        
        linuxMessageLoop.OnKeyPress += e =>
        {
            if (e.Code is LinuxVirtualKey.LeftMouse or LinuxVirtualKey.MiddleMouse or LinuxVirtualKey.RightMouse)
            {
                var button = e.Code switch
                {
                    LinuxVirtualKey.LeftMouse => MouseButton.Left,
                    LinuxVirtualKey.MiddleMouse => MouseButton.MouseWheel,
                    LinuxVirtualKey.RightMouse => MouseButton.Right,
                };
                
                var type = e.State is LinuxKeyState.KeyDown or LinuxKeyState.KeyHold
                    ? MouseButtonInputType.Down
                    : MouseButtonInputType.Up;
                
                var data = new MouseButtonEventData(
                    -1, -1, 
                    button,
                    type);
                
                OnMouseEvent?.Invoke(this, new MouseEventArgs(data));
            }
            else
            {
                var data = new KeyboardEventData(
                    Scancode.From(0), // I have no ideaFine  
                    VirtualKey.From((short)e.Code), 
                    e.State is LinuxKeyState.KeyDown or LinuxKeyState.KeyHold ? KeyInputType.KeyDown : KeyInputType.KeyUp, 
                    false, 
                    false); // The alt differentiation doesn't really exist on linux?
            
                OnKeyEvent?.Invoke(this, new KeyboardEventArgs(data));
            }
        };

        linuxMessageLoop.OnMouseMove += e =>
        {
            if (e.Axis is LinuxMouseAxis.REL_WHEEL or LinuxMouseAxis.REL_HWHEEL)
            {
                var data = new MouseWheelEventData(
                    -1, -1,
                    e.Axis == LinuxMouseAxis.REL_HWHEEL ? e.Amount : 0,
                    e.Axis == LinuxMouseAxis.REL_WHEEL ? e.Amount : 0);
                
                OnMouseEvent?.Invoke(this, new MouseEventArgs(data));
            }
            else
            {
                var data = new MouseMoveEventData(
                    e.Axis == LinuxMouseAxis.REL_X ? e.Amount : 0, 
                    e.Axis == LinuxMouseAxis.REL_Y ? e.Amount : 0);
            
                OnMouseEvent?.Invoke(this, new MouseEventArgs(data));
            }
        };

        return true;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Dispose(true);
    }

    public void Dispose(bool disposing = true)
    {
        
    }
}