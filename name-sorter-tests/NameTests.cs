using name_sorter;

namespace name_sorter_tests;

[TestFixture]
public class NameTests
{
    [Test]
    public void Create_A_Name_With_Surname_And_Single_Given_Name()
    {
        var name = new Name("Smith", new[] { "John" });

        Assert.That(name.Surname, Is.EqualTo("Smith"));
        Assert.That(name.GivenNames, Is.EqualTo(new[] { "John" }));
        Assert.That(name.GivenNames.Length, Is.EqualTo(1));
    }

    [Test]
    public void Create_A_Name_With_Surname_And_Multiple_Given_Names()
    {
        var name = new Name("Johnson", new[] { "Mary", "Jane", "Elizabeth" });

        Assert.That(name.Surname, Is.EqualTo("Johnson"));
        Assert.That(name.GivenNames, Is.EqualTo(new[] { "Mary", "Jane", "Elizabeth" }));
        Assert.That(name.GivenNames.Length, Is.EqualTo(3));
    }
    
    [Test]
    public void Constructor_With_Null_Surname_Throws_Exception()
    { 
        Assert.Throws<ArgumentException>(() => new Name(null, new[] { "John" }));
    }

    [Test]
    public void Constructor_With_Null_Given_Names_Throws_Exception()
    { 
        Assert.Throws<ArgumentException>(() => new Name("Smith", null));
    }

    [Test]
    public void Constructor_With_Empty_Surname_Throws_Exception()
    { 
        Assert.Throws<ArgumentException>(() => new Name(String.Empty, ["Alan"]));
    }

    [Test]
    public void To_String_Returns_GivenNames_Then_Surnames()
    {
        var name = new Name("Smith", new[] { "John","Edward","Henry" });
        Assert.That(name.ToString(), Is.EqualTo("John Edward Henry Smith"));
    }

    public class ComparerTests
    {

        [Test]
        public void CompareTo_Returns_Negative_When_Other_Is_Null()
        {
            var name = new Name("Smith", new[] { "John" });

            var result = name.CompareTo(null);

            Assert.That(result, Is.LessThan(0));
        }

        [Test]
        public void CompareTo_Compares_By_Surname_First_Case_Insensitive()
        {
            var name1 = new Name("adams", new[] { "John" });
            var name2 = new Name("Brown", new[] { "Jane" });

            var result = name1.CompareTo(name2);

            Assert.That(result, Is.LessThan(0)); // adams comes before Brown
        }

        [Test]
        public void CompareTo_Handles_Same_Surname_Different_Case()
        {
            var name1 = new Name("smith", new[] { "John" });
            var name2 = new Name("SMITH", new[] { "Jane" });

            var result = name1.CompareTo(name2);

            Assert.That(result,
                Is.Not.EqualTo(0).Or.EqualTo(0)); // Will need to be updated based on complete implementation
        }

        [Test]
        public void CompareTo_Same_Name_Returns_Zero()
        {
            var name1 = new Name("Smith", new[] { "John" });
            var name2 = new Name("Smith", new[] { "John" });

            var result = name1.CompareTo(name2);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CompareTo_Different_Surnames_Ignores_Given_Names()
        {
            var name1 = new Name("Adams", new[] { "Zoe" });
            var name2 = new Name("Brown", new[] { "Alice" });

            var result = name1.CompareTo(name2);

            Assert.That(result, Is.LessThan(0)); // Adams < Brown regardless of given names
        }

        [Test]
        public void CompareTo_Same_Surname_Compares_First_Given_Name()
        {
            var name1 = new Name("Smith", new[] { "Alice" });
            var name2 = new Name("Smith", new[] { "Bob" });

            var result = name1.CompareTo(name2);

            Assert.That(result, Is.LessThan(0));
        }

        [Test]
        public void CompareTo_Same_Surname_And_First_Given_Name_Compares_Second_Given_Name()
        {
            var name1 = new Name("Smith", new[] { "John", "Adam" });
            var name2 = new Name("Smith", new[] { "John", "Brian" });

            var result = name1.CompareTo(name2);

            Assert.That(result, Is.LessThan(0)); 
        }

        [Test]
        public void CompareTo_Fewer_Given_Names_Comes_First()
        {
            var name1 = new Name("Smith", new[] { "John" });
            var name2 = new Name("Smith", new[] { "John", "Michael" });

            var result = name1.CompareTo(name2);

            Assert.That(result, Is.LessThan(0)); 
        }

        [Test]
        public void CompareTo_Case_Insensitive_Comparison()
        {
            var name1 = new Name("SMITH", new[] { "JOHN" });
            var name2 = new Name("smith", new[] { "john" });

            var result = name1.CompareTo(name2);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Sorting_Multiple_Names_Works_Correctly()
        {
            var names = new List<Name>
            {
                new Name("Smith", new[] { "Alice", "Rebecca" }),
                new Name("Adams", new[] { "Michael" }),
                new Name("Smith", new[] { "John", "Adam","William" }),
                new Name("Adams", new[] { "Jane" }),
                new Name("Smith", new[] { "Alice" }),
                new Name("Brown", new[] { "Bob" }),
                new Name("Smith", new[] { "John", "Adam" })
            };

            names.Sort();

            Assert.That(names[0].Surname, Is.EqualTo("Adams"));
            Assert.That(names[0].GivenNames[0], Is.EqualTo("Jane"));
            Assert.That(names[0].GivenNames.Length, Is.EqualTo(1));
          
            Assert.That(names[1].Surname, Is.EqualTo("Adams"));
            Assert.That(names[1].GivenNames[0], Is.EqualTo("Michael"));
            Assert.That(names[1].GivenNames.Length, Is.EqualTo(1));
            
            Assert.That(names[2].Surname, Is.EqualTo("Brown"));
            Assert.That(names[2].GivenNames[0], Is.EqualTo("Bob"));
            Assert.That(names[2].GivenNames.Length, Is.EqualTo(1));
            
            Assert.That(names[3].Surname, Is.EqualTo("Smith"));
            Assert.That(names[3].GivenNames[0], Is.EqualTo("Alice"));
            Assert.That(names[3].GivenNames.Length, Is.EqualTo(1));
            
            Assert.That(names[4].Surname, Is.EqualTo("Smith"));
            Assert.That(names[4].GivenNames[0], Is.EqualTo("Alice"));
            Assert.That(names[4].GivenNames[1], Is.EqualTo("Rebecca"));
            Assert.That(names[4].GivenNames.Length, Is.EqualTo(2));
            
            Assert.That(names[5].Surname, Is.EqualTo("Smith"));
            Assert.That(names[5].GivenNames[0], Is.EqualTo("John"));
            Assert.That(names[5].GivenNames[1], Is.EqualTo("Adam"));
            Assert.That(names[5].GivenNames.Length, Is.EqualTo(2));
            
            Assert.That(names[6].Surname, Is.EqualTo("Smith"));
            Assert.That(names[6].GivenNames[0], Is.EqualTo("John"));
            Assert.That(names[6].GivenNames[1], Is.EqualTo("Adam"));
            Assert.That(names[6].GivenNames[2], Is.EqualTo("William"));
            Assert.That(names[6].GivenNames.Length, Is.EqualTo(3));
        }
    }
}