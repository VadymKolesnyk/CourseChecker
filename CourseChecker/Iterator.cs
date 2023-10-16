using System.Collections;

namespace CourseChecker;
internal class Iterator<T> : IEnumerator<T>
{
    private readonly IEnumerable<T> _items;
    private readonly IEnumerator<T> _enumerator;
    private int _index = -1;

    public T Current => throw new NotImplementedException();

    object IEnumerator.Current => throw new NotImplementedException();

    public Iterator(IEnumerable<T> items)
    {
        _items = items;
        _enumerator = _items.GetEnumerator();
    }

    public IEnumerable<T> RestWithCurrent()
    {
        return _items.Skip(_index);
    }

    public IEnumerable<T> Rest()
    {
        return _items.Skip(_index + 1);
    }

    public bool MoveNext()
    {
        var moved = _enumerator.MoveNext();
        if (moved)
        {
            _index++;
        }
        return moved;
    }

    public void Reset()
    {
        _enumerator.Reset();
        _index = -1;
    }

    public void Dispose()
    {
        _enumerator.Dispose();
    }
}
