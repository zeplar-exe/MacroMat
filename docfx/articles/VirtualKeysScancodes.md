# Virtual Keys and Scancodes

When simulating inputs, it is necessary to choose between using a 
@MacroMat.Common.VirtualKey and a @MacroMat.Common.Scancode.

Virtual keys:

- Are abstract
- Are OS-dependent
- Have a limited range of keys 

On most systems, there only exists virtual keys for common keys on a QWERTY 
keyboard. A-Z, 0-9, the F keys, modifier keys, symbols, numpad keys, and 
special keys like PrtScn and Home.

Virtual keys are most helpful when dealing with English keyboards and commonly
used keys.

On the other other hand, scancodes:

- Are concrete
- Are hardware-dependent
- Have a wider range of keys compared to virtual keys

Scancodes represent physical locations on a keyboard. As such, they are subject
to differing keyboard layouts. For detailed descriptions of various layouts,
see [kbdlayout.info](https://kbdlayout.info/).

Scancodes are useful for user-specific applications when using specific keys
not handled by MacroMat out of the box.

## Creating Virtual Keys and Scancodes

Both @MacroMat.Common.VirtualKey and @MacroMat.Common.Scancode have 
`Of` and `From` as static constructors (the latter as courtesy of 
[Vogen](https://github.com/SteveDunn/Vogen)).

In both cases, `From` take a `short` which represents the virtual key code
or scancode directly. This should only be used when working with keys that
MacroMat does not support.

On the other hand, `Of` takes a @MacroMat.Input.InputKey enum value and
translates it to the corresponding `short` and passing it to `From` 
internally. As such, usage would look like the following:

```cs
using MacroMat.Common;
using MacroMat.Input;

var aVirtual = VirtualKey.Of(InputKey.A);
var aScan = Scancode.Of(InputKey.
```

**Note that `VirtualKey.Of` bases its translation on the OS that the code is
running on.** Different OS's have different virtual keys after all.