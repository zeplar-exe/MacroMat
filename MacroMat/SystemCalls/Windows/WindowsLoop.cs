using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace MacroMat.SystemCalls.Windows;

internal class WindowsLoop : MessageLoop
{
    private Thread? Thread { get; set; }
    
    public HWND Handle => new(IntPtr.Zero);

    public WindowsLoop(Action? initialAction) : base(initialAction)
    {
        
    }

    public override void Start(Action<Exception>? exceptionCallback = null)
    {
        IsRunning = true;
        
        Thread = new Thread(() =>
        {
            UIntPtr? timerId = null;
            
            try
            {
                InitialAction?.Invoke();

                timerId = PInvoke.SetTimer(Handle, 0, 100, null);

                while (IsRunning)
                {
                    if (RequestedActions.TryDequeue(out var requested))
                        requested.Invoke();
                    
                    var result = PInvoke.GetMessage(out var message, Handle, 0, 0);

                    if (result <= 0)
                    {
                        Stop();

                        continue;
                    }
                    
                    PInvoke.TranslateMessage(message);
                    PInvoke.DispatchMessage(message);
                }
            }
            catch (Exception e)
            {
                exceptionCallback?.Invoke(e);
            }
            finally
            {
                if (timerId != null)
                    PInvoke.KillTimer(Handle, timerId.Value);
            }
        });
        
        Thread.Start();
    }
}