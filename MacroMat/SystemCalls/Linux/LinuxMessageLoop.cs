namespace MacroMat.SystemCalls.Linux;

internal class LinuxMessageLoop : MessageLoop
{
    private Queue<Action> RequestedActions { get; }
    private Thread? Thread { get; set; }
    
    private Dictionary<string, FileStream> InputStreams { get; }
    private FileSystemWatcher FileWatcher { get; }
    
    public delegate void RaiseKeyPress(LinuxKeyPressEvent e);

    public delegate void RaiseMouseMove(LinuxMouseMoveEvent e);
    
    public event RaiseKeyPress OnKeyPress;
    public event RaiseMouseMove OnMouseMove;
    
    public bool IsRunning { get; private set; }
    
    public LinuxMessageLoop(Action<MessageLoop>? initialAction) : base(initialAction)
    {
        InputStreams = new Dictionary<string, FileStream>();
        RequestedActions = new Queue<Action>();
        FileWatcher = new FileSystemWatcher("/dev/input/", "event*");

        foreach (var file in Directory.GetFiles("/dev/input/", "event*"))
        {
            InputStreams[file] = File.OpenRead(file);
        }
        
        FileWatcher.Created += (sender, args) =>
        {
            InputStreams[args.FullPath] = File.OpenRead(args.FullPath);
        };

        FileWatcher.Deleted += (sender, args) =>
        {
            InputStreams.Remove(args.FullPath);
        };
    }

    public override void Start(Action<Exception>? exceptionCallback = null)
    {
        IsRunning = true;
        
        Thread = new Thread(() =>
        {
            try
            {
                InitialAction?.Invoke(this);

                foreach (var (_, stream) in InputStreams)
                {
                    const int bufferLength = 24;
                    
                    var buffer = new byte[bufferLength];
                    var read = stream.Read(buffer, 0, bufferLength);

                    if (read != bufferLength)
                        continue;

                    var type = BitConverter.ToInt16(new[] { buffer[16], buffer[17] }, 0);
                    var code = BitConverter.ToInt16(new[] { buffer[18], buffer[19] }, 0);
                    var value = BitConverter.ToInt32(new[] { buffer[20], buffer[21], buffer[22], buffer[23] }, 0);

                    var eventType = (LinuxEventType)type;

                    switch (eventType)
                    {
                        case LinuxEventType.EV_KEY:
                            OnKeyPress?.Invoke(new LinuxKeyPressEvent((LinuxVirtualKey)code, (LinuxKeyState)value));
                            break;
                        case LinuxEventType.EV_REL:
                            OnMouseMove?.Invoke(new LinuxMouseMoveEvent((LinuxMouseAxis)code, value));
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                exceptionCallback?.Invoke(e);
            }
        });
        
        Thread.Start();
    }

    public override void EnqueueAction(Action action)
    {
        RequestedActions.Enqueue(action);
    }

    public override void Stop()
    {
        IsRunning = false;
    }

    public override void Dispose(bool disposing)
    {
        foreach (var (_, stream) in InputStreams)
        {
            stream.Dispose();
        }
    }
}