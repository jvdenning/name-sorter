using System.Collections.Immutable;
using CommandLine;

namespace name_sorter;

public class Options
{
    [Value(0, MetaName = "filename", HelpText = "File containng names to be sorted", Required = true)]
    public string? FileName { get; set; }
}

public class Program
{
    private const string SortedNamesFile = "sorted-names-list.txt";
    public static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(LoadNameFileAndSort)
            .WithNotParsed(HandleParseError);
    }

    private static void LoadNameFileAndSort(Options opts)
    {
        FileInfo fileInfo = new FileInfo(opts.FileName);
        if (!fileInfo.Exists)
        {
            Console.WriteLine($"Error: File '{opts.FileName}' does not exist");
            return;
        }

        if (fileInfo.Length == 0)
        {
            Console.WriteLine($"Error: File '{opts.FileName}' is empty");
            return;   
        }

        try
        {
            var lines = File.ReadAllLines(opts.FileName).Order().Select(n =>
            {
                Console.WriteLine(n);
                return n.ToString();
            });
            File.WriteAllLines(SortedNamesFile, lines);
     
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }
    }

    private static void HandleParseError(IEnumerable<Error> errs)
    {
        Console.WriteLine("Error: Failed to parse command line arguments");
        foreach (var error in errs)
        {
            Console.WriteLine(error.ToString());
        }
    }
}