namespace name_sorter;

public static class StringEnumerableExtensions
{
    
    public static IEnumerable<string> SurnameLastToSurnameFirst(this IEnumerable<string> names)
    {
        foreach (var name in names)
        {
            var parts = name.Split(' ');
            yield return $"{parts[^1]} {string.Join(" ", parts[..^1])}";
        }
        
    }
    
    public static IEnumerable<string> SurnameFirstToSurnameLast(this IEnumerable<string> names)
    {
        foreach (var name in names)
        {
            var parts = name.Split(' ');
            yield return $"{string.Join(" ", parts[1..])} {parts[0]}";
        }
        
    }
}