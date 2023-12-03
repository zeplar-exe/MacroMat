# Quick Start

This article gives general guidance to set up MacroMat, create macros, and run them.

## Installation

MacroMat is available as a [nuget package](example.com).

## Building Macros

The `Macro` class provides an entry point for all macro operations. Individual 
operations, `MacroInstruction`s, are executed independently by using 
@MacroMat.Instructions.MacroInstruction.Execute

```cs
using System;
using MacroMat;
using MacroMat.Instructions;

var macro = new Macro();
var pressA = KeyInputData.Press(VirtualKey.Of(InputKey.A));
var simulateKeyboard = new SimulateKeyboardInstruction(pressA);
var wait = new WaitInstruction(TimeSpan.FromMilliseconds(1000));

simulateKeyboard.Execute(macro);
wait.Execute(macro);

macro.Dispose()
```

The above example presses the A key for 1 second before disposing of the macro.
[All of the available `MacroInstruction`s can be found here.](/api/MacroMat.Instructions.html)

For convenience, a plethora of extension methods are provided for common instructions:

- PressKey
- ReleaseKey
- TapKey
- PressMouseButton
- ReleaseMouseButton
- TapMouseButton

```cs
using MacroMat;
using MacroMat.Extensions;

var macro = new Macro();

macro.MoveMouse((400, 300))
     .Wait(1000)
     .TapMouseButton()
     .Wait(100)
     .TapKey(VirtualKey.Of(InputKey.A))
     .Wait(500)
     .Dispose();
```

[The full list of extensions can be found here.](/api/MacroMat.Extensions.html)

## Notes

- It is vital that `Macro` instances are properly disposed of. Low level hooks 
can remain active after the application has closed. Hanging input hooks prevent 
MacroMat from creating more hooks should the application be rerun. They can 
also cause input lag, blue screens, or OS timeouts. Ensure that 
@MacroMat.Macro.Dispose is called before your program exits.