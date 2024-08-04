# MacroMat

MacroMat is a modern macro and hotkey creation language built with BakedEnv to expand upon AutoHotKey's syntax and features.

Features.

- Easy to use, intuitive syntax.
- Built-in support for a variety of common operations such as mouse and keyboard input, window manipulation, and more.
- Flexible and extensible design allows for easy integration with other languages and libraries.
- [Planned] Cross-platform compatibility (Windows, macOS, and Linux).
    - I do not have access to a macOS or Linux device. Contributions form those who do would be appreciated.
## Getting Started & Documentation

### C# Library

#### Installation

The C# library is be available on [nuget](nuget.org/MacroMat).

#### Usage

The library's functionality is primarily exposed via the `Macro` class, which is necessary in order to, well, macro (as a verb).

```cs
using MacroMat;

Macro macro = new Macro();
macro.EnqueueInstruction(...);

bool success = macro.ExecuteNext();

// [...]
```

*Instructions* are the building blocks of a macro. By default, several cross-platform instructions are available in the `MacroMat.Instructions` namespace; `SimulateKeyboardInstruction`, `SimulateMouseInstruction`, `SendUnicodeInstruction`, `KeyCallbackInstruction`, and more.

```cs
// [...]

var pressA = new SimulateKeyboardInstruction(KeyInputData.FromKey(InputKey.A, KeyInputType.KeyDown));
var releaseA = new SimulateKeyboardInstruction(KeyInputData.FromKey(InputKey.A, KeyInputType.KeyUp));

macro.EnqueueInstruction(pressA)
     .EnqueueInstruction(releaseA);

macro.ExecuteAll();
```

For convenience, several extension methods are available for common operations.

```cs
using MacroMat.Extensions;

// [...]

macro.SimulateUnicode("Unicode, UTF-8, or is it UTF-16?")
     .Wait(1000)
     .Action(() => Console.WriteLine("Checkpoint."))
     .Wait(500)
     .SimulateInput(KeyInputData.FromKey(InputKey.Backspace, KeyInputType.KeyDown))
     .Wait(5000); // Hold down backspace for 5 seocnds
     .SimulateInput(KeyInputData.FromKey(InputKey.Backspace, KeyInputType.KeyUp))
```

See the [documentation](example.com) or the extended [Getting Started](example.com) guide.
