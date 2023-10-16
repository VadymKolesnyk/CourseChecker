using System.Diagnostics;

namespace CourseChecker.Items;
[DebuggerDisplay("{Number}")]
internal class ControlPoint : IItem
{
    public string Number { get; }

    public string ErrorIfEmpty => Number;

    public ControlPoint(string number)
    {
        Number = number;
    }

    public bool Check(IEnumerable<string> points, out IEnumerable<string> rest, out string pointError)
    {
        if (points.Any() && points.First() == Number)
        {
            rest = points.Skip(1);
            pointError = null;
            return true;
        }
        rest = points;
        pointError = Number;
        return false;
    }

    public override string ToString() => Number;

}
