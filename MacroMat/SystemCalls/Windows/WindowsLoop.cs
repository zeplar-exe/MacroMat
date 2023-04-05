namespace MacroMat.SystemCalls.Windows;

internal class WindowsLoop : MessageLoop
{
    private Thread? Thread { get; set; }
    
    public override IntPtr Handle => IntPtr.Zero;

    public WindowsLoop(Action? initialAction) : base(initialAction)
    {
        
    }

    public override void Start(Action<Exception>? exceptionCallback = null)
    {
        IsRunning = true;
        
        Thread = new Thread(() =>
        {
            IntPtr? timerId = null;
            
            try
            {
                InitialAction?.Invoke();

                timerId = Win32.SetTimer(Handle, IntPtr.Zero, 100, IntPtr.Zero);

                while (IsRunning)
                {
                    if (RequestedActions.TryDequeue(out var requested))
                        requested.Invoke();
                    
                    var result = Win32.GetMessage(out var message, Handle, 0, 0);

                    if (result <= 0)
                    {
                        Stop();

                        continue;
                    }
                    
                    Win32.TranslateMessage(ref message);
                    Win32.DispatchMessage(ref message);
                }
            }
            catch (Exception e)
            {
                exceptionCallback?.Invoke(e);
            }
            finally
            {
                if (timerId != null)
                    Win32.KillTimer(Handle, timerId.Value);
            }
        });
        
        Thread.Start();
    }
}