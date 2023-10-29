# Keyboard & Mouse Callbacks

Alongside simulation, MacroMat offers the ability to catch and react to keyboard and mouse input.

## Callbacks

`MacroInstruction`s exist for hooking into the keyboard and mouse.

- `KeyCallbackInstruction`
- `MouseCallbackInstruction`

Both of which function effectively the same, besides their expected parameters.

```cs
using MacroMat;
using MacroMat.
```

## InputWatcher

In addition, the `InputWatcher` class is a 'wrapper' which keeps track of pressed keys and mouse buttons. It allows for implementing hotkeys, for example.

```cs
using MacroMat;
using MacroMat.Input;

var macro = new MacroMat();
var watcher = new InputWatcher(macro);

var hotkeyInput = KeyInputData.Release(
    VirtualKey.Of(InputKey.LeftControl, InputKey.C));
var hotkey = macro.AddKeyCallback(hotkeyInput, () => {
    Console.WriteLine("TODO: Copy Functionality")
});

macro.Wait(10000)
     .Action(() => hotkey.Dispose())
     .Dispose();
```

## Input Hook Auto-Initialization

When creating a `Macro`, keyboard and mouse hooks are automatically created by default. To change this, supply false to the constructor:

```cs
using MacroMat;

var macro = new Macro(false);

// [...]
```

In order to use keyboard and mouse hooks in the future, call `MacroMat.Initialize()`.