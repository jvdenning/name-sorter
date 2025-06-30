namespace name_sorter;

public class Name : IComparable<Name>
{
    private const StringComparison DefaultStringComparison = StringComparison.InvariantCultureIgnoreCase;
    public string Surname { get; }
    public string[] GivenNames { get; }

    public Name(string surname, string[] givenNames)
    {        if (string.IsNullOrWhiteSpace(surname))
        {
            throw new ArgumentException("Surname cannot be null or empty", nameof(surname));
        }
        
        if (givenNames == null || givenNames.Length == 0)
        {
            throw new ArgumentException("At least one given name is required", nameof(givenNames));
        }
        
        if (givenNames.Any(name => string.IsNullOrWhiteSpace(name)))
        {
            throw new ArgumentException("Given names cannot be null or empty", nameof(givenNames));
        }

        Surname = surname;
        GivenNames = givenNames;

    }

    public int CompareTo(Name? other)
    {
        if(other == null)
        {
            return -1;
        }

        if (String.Compare(Surname, other.Surname, DefaultStringComparison) != 0)
        {
            return String.Compare(Surname, other.Surname, DefaultStringComparison);
        }
        
        
        var minLength = Math.Min(GivenNames.Length, other.GivenNames.Length);
        
        for (int i = 0; i < minLength; i++)
        {
            var givenNameComparison = String.Compare(GivenNames[i], other.GivenNames[i], DefaultStringComparison);
            if (givenNameComparison != 0)
            {
                return givenNameComparison;
            }
        }
        
        return GivenNames.Length.CompareTo(other.GivenNames.Length);
    }
    
    public override string ToString()
    {
        return $"{Surname} {string.Join(" ", GivenNames)}";
    }

    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }
}