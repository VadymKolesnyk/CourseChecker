namespace CourceChecker.Items;
internal class AnyOfBlock : IItem
{
    private readonly int _amount;
    private readonly IItem[] _items;

    public AnyOfBlock(int amount, params IItem[] items)
    {
        _amount = amount;
        _items = items;
    }
    public string ErrorIfEmpty => _items.First().ErrorIfEmpty;

    public bool Check(IEnumerable<string> points, out IEnumerable<string> rest, out string pointError)
    {
        var unused = _items.ToList();
        var matchedItems = new List<IItem>();
        var iterator = points.GetIterator();
        while (iterator.MoveNext())
        {
            IEnumerable<string> restInner = null;
            var matched = unused.FirstOrDefault(x => x.Check(iterator.RestWithCurrent(), out restInner));

            if (matched is not null)
            {
                iterator = restInner.GetIterator();
                unused.Remove(matched);
                matchedItems.Add(matched);
                if (matchedItems.Count == _amount)
                {
                    rest = iterator.Rest();
                    pointError = null;
                    return true;
                }
            }
        }
        rest = points;
        pointError = unused.First().ErrorIfEmpty;
        return false;
    }

    public override string ToString() => $"[{_amount} {string.Join(' ', _items.Select(x => x.ToString()))}]";

}
