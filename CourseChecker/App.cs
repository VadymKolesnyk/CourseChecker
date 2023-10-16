namespace CourseChecker;
class App
{
    private readonly string _path;
    private readonly TimeSpan _period;
    private readonly Provider _provider;
    private readonly Checker _checker;

    private int _lastChecked = -1;

    public App(
        string path,
        TimeSpan period,
        IDictionary<string, string> groups)
    {
        _path = path;
        _period = period;

        _provider = new Provider(Path.Combine(path, "SPLIT.dbf"), Path.Combine(path, "OLD.dbf"));

        var logPath = Path.Combine(path, "Log.txt");

        _checker = new Checker(_provider, groups)
        {
            Log = message =>
            {
                Console.WriteLine(message);
                File.AppendAllLines(logPath, new[] { $"[{DateTime.Now:G}] {message}" });

            }
        };
    }

    public void Start(bool skipExisting)
    {
        _checker.Log?.Invoke("Course checker has been successfully started");
        _checker.Log?.Invoke($"Working in '{_path}'");
        _checker.Log?.Invoke($"Type sportsmen's number to recheck them.");
        _checker.Log?.Invoke($"Type exit to close program.");
        _checker.Log?.Invoke($"");

        _lastChecked = !skipExisting ? _checker.CheckAll() : _checker.CheckLast();

        Task.Run(CheckLastAsync);

        while (true)
        {
            var input = Console.ReadLine();
            if (input == "exit")
            {
                return;
            }
            if (int.TryParse(input, out var id))
            {
                _checker.CheckById(id);
            }
            else
            {
                Console.WriteLine("Invalid number");
            }

        }
    }

    public async Task CheckLastAsync()
    {

        while (true)
        {
            try
            {
                var split = _provider.GetLastSplit();
                if (split is not null && split.Id != _lastChecked)
                {
                    _checker.CheckById(split.Id);
                    _lastChecked = split.Id;
                }
                await Task.Delay(_period);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}
