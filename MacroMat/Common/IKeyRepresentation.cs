namespace MacroMat.Common;

/// <summary>
/// Unifying interface for <see cref="VirtualKey"/> and <see cref="Scancode"/>.
/// Classes that implement this interface other than VirtualKey and Scancode
/// are moot when used with existing library methods.
/// </summary>
public interface IKeyRepresentation
{
    
}