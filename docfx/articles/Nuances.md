# Nuances

A list of nuances and pitfalls to watch out for while using MacroMat.

- **Windows**
    - Input simulation usually lasts a couple milliseconds. To account for 
this, give at least 50ms before disposing a macro or performing an action 
which relies on the input.
    - Long running keyboard and mouse hooks are not recommended. Should a 
callback run for longer than ~500 milliseconds, there will be extreme input 
lag for the rest of the system. After ~3 seconds, the hook will be discarded 
entirely and a reset of the application is necessary. Instead, callbacks 
should either run their code in another thread or otherwise keep execution 
time to a minimum.