namespace CourseChecker;
internal static class Extensions
{
    public static Iterator<T> GetIterator<T>(this IEnumerable<T> items)
    {
        return new Iterator<T>(items);
    }
}
