# Keyboard & Mouse Callbacks

Alongside simulation, MacroMat offers the ability to catch and react to keyboard and mouse input.

## Callbacks

`MacroInstruction`s exist for hooking into the keyboard and mouse.

- `KeyCallbackInstruction`
- `MouseCallbackInstruction`

Both of which function effectively the same, besides their expected parameters.

```cs
using MacroMat;
using MacroMat.Instructions;


```

## InputWatcher

In addition, the @MacroMat.InputWatcher class is a 'wrapper' which keeps track of 
pressed keys and mouse buttons. It allows for implementing hotkeys, for 
example:

```cs
using MacroMat;
using MacroMat.Input;

var macro = new MacroMat();
var watcher = new InputWatcher(macro);

var hotkeyInput = KeyInputData.Release(
    VirtualKey.Of(InputKey.LeftControl, InputKey.C));

var hotkey = watcher.AddKeyCallback(hotkeyInput, () => {
    Console.WriteLine("TODO: Copy Functionality")
});

watcher.Dispose();
macro.Dipose();
```TEEEEEEEEEEEEEEST, also change initializatin to use an enum None | Key | Mouse | All

Individual callbacks can be disposed of as well:

```cs
// [...]

var hotkey = watcher.AddKeyCallback(hotkeyInput, () => {
    Console.WriteLine("TODO: Copy Functionality")
});

hotkey.Dispose();
```

## Input Hook Auto-Initialization

When creating a `Macro`, keyboard and mouse hooks are automatically created by 
default. To change this, supply false to the constructor:

```cs
using MacroMat;

var macro = new Macro(false);

// [...]
```

In order to use keyboard and mouse hooks on a macro constructed this way, call 
@MacroMat.Macro.Initialize.