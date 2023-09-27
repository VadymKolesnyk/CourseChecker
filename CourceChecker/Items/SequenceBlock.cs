namespace CourceChecker.Items;
internal class SequenceBlock : IItem
{
    private readonly IItem[] _items;

    public string ErrorIfEmpty => _items.First().ErrorIfEmpty;

    public SequenceBlock(params IItem[] items)
    {
        _items = items;
    }

    public bool Check(IEnumerable<string> points, out IEnumerable<string> rest, out string pointError)
    {
        var iterator = points.GetIterator();
        foreach (var item in _items)
        {
            if (!FindCorrect(iterator, item, out var restInner, out pointError))
            {
                rest = points;
                return false;
            }
            iterator = restInner.GetIterator();
        }
        rest = iterator.Rest();
        pointError = null;
        return true;

    }

    private static bool FindCorrect(Iterator<string> iterator, IItem item, out IEnumerable<string> rest, out string pointError)
    {
        pointError = item.ErrorIfEmpty;
        while (iterator.MoveNext())
        {
            if (item.Check(iterator.RestWithCurrent(), out rest, out pointError))
            {
                return true;
            }
        }
        rest = null;
        return false;
    }

    public override string ToString() => $"<{string.Join(' ', _items.Select(x => x.ToString()))}>";

}
