using MacroMat.Input;
using MacroMat.Instructions;

namespace MacroMat.Extensions;

public static class MacroMouseCallbackExtensions
{
    public static Macro OnMouseEvent(this Macro macro, Action<MouseEventArgs> action)
    {
        return OnMouseEvent(macro, _ => true, action);
    }
    
    public static Macro OnMouseEvent(this Macro macro, 
        Func<MouseEventData, bool> predicate, Action<MouseEventArgs> action)
    {
        new MouseCallbackInstruction(predicate, action).Execute(macro);
        
        return macro;
    }
    
    public static Macro OnMouseButton(this Macro macro, Action<MouseEventArgs, MouseButtonEventData> action)
    {
        return macro.OnMouseEvent(
            _ => true,
            args =>
            {
                action.Invoke(args, (MouseButtonEventData)args.Data);
            });
    }

    public static Macro OnMouseButton(this Macro macro,
        Func<MouseButtonEventData, bool> predicate, Action<MouseEventArgs, MouseButtonEventData> action)
    {
        return macro.OnMouseEvent(
            data =>
            {
                if (data is MouseButtonEventData eventData) 
                    return predicate.Invoke(eventData);

                return false;
            },
            args =>
            {
                action.Invoke(args, (MouseButtonEventData)args.Data);
            });
    }
    
    public static Macro OnMouseScroll(this Macro macro, Action<MouseEventArgs, MouseButtonEventData> action)
    {
        return macro.OnMouseEvent(
            _ => true,
            args =>
            {
                action.Invoke(args, (MouseButtonEventData)args.Data);
            });
    }

    public static Macro OnMouseScroll(this Macro macro,
        Func<MouseWheelEventData, bool> predicate, Action<MouseEventArgs, MouseWheelEventData> action)
    {
        return macro.OnMouseEvent(
            data =>
            {
                if (data is MouseWheelEventData eventData) 
                    return predicate.Invoke(eventData);

                return false;
            },
            args =>
            {
                action.Invoke(args, (MouseWheelEventData)args.Data);
            });
    }
    
    public static Macro OnMouseMove(this Macro macro, Action<MouseEventArgs, MouseButtonEventData> action)
    {
        return macro.OnMouseEvent(
            _ => true,
            args =>
            {
                action.Invoke(args, (MouseButtonEventData)args.Data);
            });
    }

    public static Macro OnMouseMove(this Macro macro,
        Func<MouseMoveEventData, bool> predicate, Action<MouseEventArgs, MouseMoveEventData> action)
    {
        return macro.OnMouseEvent(
            data =>
            {
                if (data is MouseMoveEventData eventData) 
                    return predicate.Invoke(eventData);

                return false;
            },
            args =>
            {
                action.Invoke(args, (MouseMoveEventData)args.Data);
            });
    }
}