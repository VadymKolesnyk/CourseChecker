using CourceChecker.Items;
using CourceChecker.Models;

namespace CourceChecker;
internal class Checker
{
    private readonly Provider _provider;

    private readonly IDictionary<string, IItem> _groups;

    public Action<string> Log { get; set; }

    public Checker(Provider provider, IDictionary<string, string> groups)
    {
        _provider = provider;
        _groups = groups.ToDictionary(x => x.Key, x => Parser.Parse(x.Value));
    }

    public int CheckAll()
    {
        var splits = _provider.GetAllSplits();
        foreach (var split in splits)
        {
            CheckOne(split);
        }
        return splits.Last().Id;

    }

    public int CheckLast()
    {
        var split = _provider.GetLastSplit();
        CheckOne(split);
        return split.Id;
    }

    public void CheckById(int id)
    {
        var split = _provider.GetSplitById(id);
        CheckOne(split);
    }

    void CheckOne(Split split)
    {
        try
        {
            var order = _groups.TryGetValue(split.Group, out var value) ? value : throw new Exception($"Undefined group '{split.Group}'");
            var correct = order.Check(split.Points, out string pointError);
            if (correct)
            {
                _provider.UpdateSplit(split.Id, false, "");
                _provider.UpdateOldRecord(split.Nomer, false);
            }
            else
            {
                _provider.UpdateSplit(split.Id, true, pointError);
                _provider.UpdateOldRecord(split.Nomer, true);
            }
            Log?.Invoke($"{split.Nomer,-3} | {(correct ? "Ok" : "Error on " + pointError),-12} | {split.SportsmanName,-23} | {split.Group,-5} | ");
        }
        catch (Exception e)
        {
            Log?.Invoke($"{split.Nomer,-3} |              | {split.SportsmanName,-23} | {split.Group,-5} | {e.Message}");
        }

    }
}
