namespace CourseChecker.Items;
internal interface IItem
{
    bool Check(IEnumerable<string> points, out IEnumerable<string> rest, out string pointError);
    bool Check(IEnumerable<string> points, out string pointError)
    {
        return Check(points, out var _, out pointError);
    }

    bool Check(IEnumerable<string> points, out IEnumerable<string> rest)
    {
        return Check(points, out rest, out var _);
    }

    bool Check(IEnumerable<string> points)
    {
        return Check(points, out var _, out var _);
    }
    string ErrorIfEmpty { get; }


}
