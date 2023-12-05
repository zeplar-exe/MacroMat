# What is MacroMat?

MacroMat is a cross-platform macro and automation library. At heart, it is made
to achieve most tasks relating to input. For example:

- Remapping keys
- Detecting key presses
- Detecting mouse clicks
- Simulating keyboard input

The C# library is built with ease of access in mind.

```cs
using MacroMat;
using MacroMat.Input;
using MacroMat.Extensions;

var macro = new Macro();

macro.TapUnicode("Press P to play poker.");
macro.OnKey(InputKey.X, KeyInputType.Pressed, args => {
    PlayPoker();
});
```

MacroMat implements input handling for Windows, MacOS, and Linux.