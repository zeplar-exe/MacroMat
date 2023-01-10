using System.Runtime.InteropServices;

namespace MacroMat.SystemCalls.Windows;

public class WindowsLoop : MessageLoop
{
    [DllImport("user32.dll")]
    private static extern int GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin,
        uint wMsgFilterMax);
    
    [DllImport("user32.dll", ExactSpelling=true)]
    public static extern IntPtr SetTimer(IntPtr hWnd, IntPtr nIdEvent, uint uElapse, IntPtr lpTimerFunc);
    
    [DllImport("user32.dll", ExactSpelling=true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool KillTimer(IntPtr hWnd, IntPtr uIdEvent);
    
    [DllImport("user32.dll")]
    private static extern bool TranslateMessage([In] ref MSG lpMsg);
    
    [DllImport("user32.dll")]
    private static extern IntPtr DispatchMessage([In] ref MSG lpmsg);
    
    [StructLayout(LayoutKind.Sequential)]
    public struct MSG
    {
        IntPtr hwnd;
        uint message;
        UIntPtr wParam;
        IntPtr lParam;
        int time;
        POINT pt;
        int lPrivate;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static implicit operator System.Drawing.Point(POINT p)
        {
            return new System.Drawing.Point(p.X, p.Y);
        }

        public static implicit operator POINT(System.Drawing.Point p)
        {
            return new POINT(p.X, p.Y);
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }
    }
    
    private Thread? Thread { get; set; }

    public WindowsLoop(Action initialAction) : base(initialAction)
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
                InitialAction.Invoke();

                timerId = SetTimer(IntPtr.Zero, IntPtr.Zero, 100, IntPtr.Zero);

                while (IsRunning)
                {
                    var result = GetMessage(out var message, IntPtr.Zero, 0, 0);

                    if (result <= 0)
                    {
                        Stop();

                        continue;
                    }

                    TranslateMessage(ref message);
                    DispatchMessage(ref message);
                }
            }
            catch (Exception e)
            {
                exceptionCallback?.Invoke(e);
            }
            finally
            {
                if (timerId != null)
                    KillTimer(IntPtr.Zero, timerId.Value);
            }
        });
        
        Thread.Start();
    }
}