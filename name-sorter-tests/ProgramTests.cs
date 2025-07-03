using name_sorter;
using Program = name_sorter.Program;

namespace name_sorter_tests;

[TestFixture]
public class ProgramTests
{

    const string SortedNameFile = "sorted-names-list.txt";

    [Test]
    public void Program_Should_Handle_FourGivenNames()
    {
        var testNames = new[]
        {
            "Elizabeth Alexandra Mary Victoria Windsor",
            "John Fitzgerald Patrick Joseph Kennedy",
            "Franklin Delano Theodore James Roosevelt"
        };

        var tempFileName = Path.GetTempFileName();

        try
        {
            File.WriteAllLines(tempFileName, testNames);

            Program.Main([tempFileName]);
            var fileLines = File.ReadAllLines(SortedNameFile);

            Assert.That(fileLines.Length, Is.EqualTo(3));

            foreach (var line in fileLines)
            {
                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                Assert.That(parts.Length, Is.EqualTo(5)); 
            }
        }
        finally
        {
            if (File.Exists(tempFileName))
            {
                File.Delete(tempFileName);
            }
        }
    }

    [Test]
    public void Program_Should_Handle_MixedNameLengths()
    {
        var testNames = new[]
        {
            "John Adams", // 1 given name
            "Sarah Elizabeth Barker", // 2 given names
            "Michael James Robert Clark", // 3 given names
            "Mary Catherine Louise Anne Edwards" // 4 given names
        };

        var tempFileName = Path.GetTempFileName();

        try
        {
            File.WriteAllLines(tempFileName, testNames);
            Program.Main([tempFileName]);

            var fileLines = File.ReadAllLines(SortedNameFile);
            Assert.That(fileLines.Length, Is.EqualTo(4));

            // Verify each line has the expected number of parts
            var expectedParts = new[] { 2, 3, 4, 5 };
            for (int i = 0; i < fileLines.Length; i++)
            {
                var parts = fileLines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                Assert.That(parts.Length, Is.EqualTo(expectedParts[i]));
            }
        }
        finally
        {
            if (File.Exists(tempFileName))
            {
                File.Delete(tempFileName);
            }
        }
    }

    [Test]
    public void Program_Should_Handle_EmptyFile()
    {
        var tempFileName = Path.GetTempFileName();

        try
        {
            File.Delete(SortedNameFile);
            Program.Main([tempFileName]);
            Assert.That(File.Exists(SortedNameFile), Is.False);
        }
        finally
        {
            if (File.Exists(tempFileName))
            {
                File.Delete(tempFileName);
            }
        }
    }

    [Test]
    public void Program_Should_Output_Sorted_Names_To_File()
    {
        var testNames = new[]
        {
            "Robert Michael James Williams",
            "John Smith",
            "Mary Elizabeth Johnson"
        };

        var tempFileName = Path.GetTempFileName();

        try
        {
            File.WriteAllLines(tempFileName, testNames);

            Program.Main([tempFileName]);

            var names = File.ReadLines(SortedNameFile).ToArray();

            Assert.That(names.Length, Is.EqualTo(testNames.Length));

            Assert.That(names[0], Is.EqualTo(testNames[2]));
            Assert.That(names[1], Is.EqualTo(testNames[1]));
            Assert.That(names[2], Is.EqualTo(testNames[0]));
        }
        finally
        {
            if (File.Exists(tempFileName))
            {
                File.Delete(tempFileName);
            }
        }
    }


    [Test]
    public void Program_Should_Overwrite_Sorted_Names_File()
    {
        var testNames1 = new[]
        {
            "Robert Michael James Williams",
            "John Smith",
            "Mary Elizabeth Johnson"
        };

        var testNames2 = new[]
        {
            "Ruby Wax",
            "Fred Bassett"
        };

        var tempFileName = Path.GetTempFileName();

        try
        {
            File.WriteAllLines(tempFileName, testNames1);
            Program.Main([tempFileName]);

            File.WriteAllLines(tempFileName, testNames2);
            Program.Main([tempFileName]);

            var names = File.ReadLines(SortedNameFile).ToArray();

            Assert.That(names.Length, Is.EqualTo(2));
            Assert.That(names[0], Is.EqualTo(testNames2[1]));
            Assert.That(names[1], Is.EqualTo(testNames2[0]));
         
        }
        finally
        {
            if (File.Exists(tempFileName))
            {
                File.Delete(tempFileName);
            }
        }
    }

    [Test]
    public void Program_Should_Handle_Large_Dataset_With_10000_Names()
    {
        var random = new Random(42); 
       
        var surnames = new[]
        {
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez",
            "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson",
            "Martin",
            "Lee", "Perez", "Thompson", "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson",
            "Walker", "Young", "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores",
            "Green", "Adams", "Nelson", "Baker", "Hall", "Rivera", "Campbell", "Mitchell", "Carter", "Roberts"
        };

        var givenNames = new[]
        {
            "James", "Robert", "John", "Michael", "David", "William", "Richard", "Joseph", "Thomas", "Christopher",
            "Charles", "Daniel", "Matthew", "Anthony", "Mark", "Donald", "Steven", "Paul", "Andrew", "Kenneth",
            "Mary", "Patricia", "Jennifer", "Linda", "Elizabeth", "Barbara", "Susan", "Jessica", "Sarah", "Karen",
            "Lisa", "Nancy", "Betty", "Helen", "Sandra", "Donna", "Carol", "Ruth", "Sharon", "Michelle",
            "Laura", "Sarah", "Kimberly", "Deborah", "Jessica", "Shirley", "Cynthia", "Angela", "Melissa", "Brenda",
            "Emma", "Olivia", "Ava", "Isabella", "Sophia", "Charlotte", "Mia", "Amelia", "Harper", "Evelyn"
        };

        var testNames = new List<string>();
       
        for (int i = 0; i < 10000; i++)
        {
            var surname = surnames[random.Next(surnames.Length)];
            var numGivenNames = random.Next(1, 6); 

            var nameBuilder = new List<string> { surname };

            for (int j = 0; j < numGivenNames; j++)
            {
                var givenName = givenNames[random.Next(givenNames.Length)];
                nameBuilder.Add(givenName);
            }

            testNames.Add(string.Join(" ", nameBuilder));
        }

        var tempFileName = Path.GetTempFileName();
    

        try
        {
            File.WriteAllLines(tempFileName, testNames);
            Console.WriteLine($"Generated {testNames.Count} names in test file");

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            
            Program.Main([tempFileName]);
            stopwatch.Stop();

            var sortedLines = File.ReadAllLines(SortedNameFile);

            Assert.That(sortedLines.Length, Is.EqualTo(10000), "Should have processed all 10,000 names"); Console.WriteLine(
                $"Successfully processed and verified {testNames.Count} names in {stopwatch.ElapsedMilliseconds}ms");
            Console.WriteLine(
                $"Average processing time: {(double)stopwatch.ElapsedMilliseconds / testNames.Count:F4}ms per name");
        }
        finally
        {
            if (File.Exists(tempFileName))
            {
                File.Delete(tempFileName);
            }

            if (File.Exists(SortedNameFile))
            {
                File.Delete(SortedNameFile);
            }
        }
    }
}