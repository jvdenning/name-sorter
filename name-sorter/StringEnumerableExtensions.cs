namespace name_sorter;

public static class StringEnumerableExtensions
{
    public static IEnumerable<Name> ToNames(this IEnumerable<string> names)
    {

        foreach (var name in names)
        {
            var parts = name.Split(' ');
            yield return new Name(parts[0], parts.Skip(1).ToArray());
        }
        
    }
}