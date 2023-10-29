# Nuances

A list of nuances and pitfalls to watch out for while using MacroMat.

- **Windows**
    - Input simulation usually lasts a couple milliseconds. To account for this, give at least 50ms before disposing a macro or performing an action which relies on the input.
    - Slow, long running keyboard and mouse hooks are not reccomended. Should a callback run for longer than a few milliseconds, there will be extreme input lag for the rest of the system. After ~3 seconds, the hook will be discarded and a require restart of the application is required. Instead, callbacks should either run their code in another thread, or otherwise keep execution time to a minimum.