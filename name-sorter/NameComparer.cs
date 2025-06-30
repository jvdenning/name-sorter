namespace name_sorter;

public class NameComparer : Comparer<Name>
{
    private const StringComparison DefaultStringComparison = StringComparison.InvariantCultureIgnoreCase;
    
    public override int Compare(Name? x, Name? y)
    {
        if(x == null || y == null)
        {
            return -1;
        }

        if (String.Compare(x.Surname, y.Surname, DefaultStringComparison) != 0)
        {
            return String.Compare(x.Surname, y.Surname, DefaultStringComparison);
        }
        
        var minLength = Math.Min(x.GivenNames.Length, y.GivenNames.Length);
        
        for (int i = 0; i < minLength; i++)
        {
            var givenNameComparison = String.Compare(x.GivenNames[i], y.GivenNames[i], DefaultStringComparison);
            if (givenNameComparison != 0)
            {
                return givenNameComparison;
            }
        }
        
        return x.GivenNames.Length.CompareTo(y.GivenNames.Length);
    }
}