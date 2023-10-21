using MacroMat.Common;
using MacroMat.Extensions;
using MacroMat.Input;

namespace MacroMat;

public class InputWatcher : IDisposable
{
    private HashSet<IKeyRepresentation> PressedKeys { get; }
    private HashSet<MouseButton> PressedMouseButtons { get; }

    private Macro Macro { get; }
    private Dictionary<KeyInputData, List<Action>> KeyCallbacks { get; }
    private Dictionary<MouseInputData, List<Action>> MouseCallbacks { get; }

    public InputWatcher(Macro macro)
    {
        PressedKeys = new HashSet<IKeyRepresentation>();
        PressedMouseButtons = new HashSet<MouseButton>();
        Macro = macro;
        KeyCallbacks = new Dictionary<KeyInputData, List<Action>>();
        MouseCallbacks = new Dictionary<MouseInputData, List<Action>>();

        macro.OnKeyEvent(args =>
        {
            var scancode = args.Data.HardwareScancode;
            var virtualKey = args.Data.VirtualCode;
            var oldPressedKeys = new HashSet<IKeyRepresentation>(PressedKeys);
            
            if (args.Data.Type == KeyInputType.KeyDown)
            {
                PressedKeys.Add(scancode);
                PressedKeys.Add(virtualKey);
            }
            else if (args.Data.Type == KeyInputType.KeyUp)
            {
                PressedKeys.Remove(scancode);
                PressedKeys.Remove(virtualKey);
            }
            
            foreach (var (inputData, actions) in KeyCallbacks)
            {
                bool AllPressedIn(HashSet<IKeyRepresentation> keys)
                {
                    return inputData.Keys.All(keys.Contains);
                }
                
                // inputData is for pressed and all matches all currently pressed keys
                // OR
                // keyInput data is for released, it was previously pressed, but not at least one of its keys has been released
                if (inputData.Type == KeyInputType.KeyDown && AllPressedIn(PressedKeys) ||
                    (inputData.Type == KeyInputType.KeyUp && AllPressedIn(oldPressedKeys) && !AllPressedIn(PressedKeys)))
                {
                    actions.ForEach(a => a.Invoke());
                }
            }
        });

        macro.OnMouseEvent(args =>
        {
            var button = args.Data.Button;
            var oldPressedButtons = new HashSet<MouseButton>(PressedMouseButtons);
            
            if (args.Data.Type == MouseInputType.Up)
            {
                PressedMouseButtons.Add(button);
            }
            else if (args.Data.Type == MouseInputType.Down)
            {
                PressedMouseButtons.Remove(button);
            }
            
            foreach (var (inputData, actions) in MouseCallbacks)
            {
                bool AllPressedIn(IEnumerable<MouseButton> buttons)
                {
                    return inputData.Buttons.All(buttons.Contains);
                }
                
                // inputData is for pressed and all matches all currently pressed keys
                // OR
                // keyInput data is for released, it was previously pressed, but not at least one of its keys has been released
                if (inputData.Type == MouseInputType.Down && AllPressedIn(PressedMouseButtons) ||
                    (inputData.Type == MouseInputType.Up && AllPressedIn(oldPressedButtons) && !AllPressedIn(PressedMouseButtons)))
                {
                    actions.ForEach(a => a.Invoke());
                }
            }
        });
    }
    
    public bool IsKeyUp(IKeyRepresentation key)
    {
        return PressedKeys.Contains(key);
    }
    
    public bool IsKeyDown(IKeyRepresentation key)
    {
        return PressedKeys.Contains(key);
    }

    public IDisposable AddKeyCallback(KeyInputData keyInput, Action action)
    {
        if (KeyCallbacks.TryGetValue(keyInput, out var list))
        {
            list.Add(action);
        }
        else
        {
            KeyCallbacks[keyInput] = new List<Action> { action };
        }

        return new ActionDisposable(() =>
        {
            if (KeyCallbacks.TryGetValue(keyInput, out var actions))
            {
                actions.Remove(action);
            }
        });
    }
    
    public IDisposable AddMouseCallback(MouseInputData mouseInput, Action action)
    {
        if (MouseCallbacks.TryGetValue(mouseInput, out var list))
        {
            list.Add(action);
        }
        else
        {
            MouseCallbacks[mouseInput] = new List<Action> { action };
        }

        return new ActionDisposable(() =>
        {
            if (MouseCallbacks.TryGetValue(mouseInput, out var actions))
            {
                actions.Remove(action);
            }
        });
    }

    public void RemoveCallback(KeyInputData data)
    {
        KeyCallbacks.Remove(data);
    }
    
    public void RemoveCallback(MouseInputData data)
    {
        MouseCallbacks.Remove(data);
    }

    public void Dispose()
    {
        Macro.Dispose();
    }
}