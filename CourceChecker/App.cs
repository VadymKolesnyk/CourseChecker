namespace CourceChecker;
class App
{
    private readonly TimeSpan _period;
    private readonly Provider _provider;
    private readonly Checker _checker;

    private int _lastChecked = -1;

    public App(
        string splitPath,
        string oldFPath,
        TimeSpan period,
        IDictionary<string, string> groups)
    {
        _period = period;

        _provider = new Provider(splitPath, oldFPath);
        _checker = new Checker(_provider, groups)
        {
            Log = Console.WriteLine
        };
    }

    public void Start(bool skipExisting)
    {
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
            var split = _provider.GetLastSplit();
            if (split.Id != _lastChecked)
            {
                _checker.CheckById(split.Id);
                _lastChecked = split.Id;
            }
            await Task.Delay(_period);
        }
    }
}
