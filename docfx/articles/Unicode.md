# Unicode

MacroMat supports unicode input as opposed to using InputKeys. This can be done
with the SimulateUnicodeInstruction and its corresponding extension like so:

```cs
using MacroMat;
using MacroMat.Instructions;
using MacroMat.Extensions;

var macro = new Macro();
var press = new SimulateUnicodeInstruction("Hello world!", KeyInputType.KeyDown);
var release = new SimulateUnicodeInstruction("Hello world!", KeyInputType.KeyUp);

press.Execute(macro);
release.Execute(macro);

// or...

macro.TapUnicode("Hello world! ☩ ⾇ ⛸");
```

Any unicode character is valid.