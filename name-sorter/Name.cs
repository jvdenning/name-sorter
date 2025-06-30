namespace name_sorter;

public class Name
{
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
        
        if (givenNames.Any(string.IsNullOrWhiteSpace))
        {
            throw new ArgumentException("Given names cannot be null or empty", nameof(givenNames));
        }

        Surname = surname;
        GivenNames = givenNames;

    }
    
    public override string ToString()
    {
        return $"{string.Join(" ", GivenNames)} {Surname}";
    }

    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }
}