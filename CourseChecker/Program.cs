using CommandLine;
using CourseChecker.ConsoleApp;
using Newtonsoft.Json;
using System.Text;

namespace CourseChecker;

public static class Program
{

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        try
        {
            if (args.Length == 0)
            {
                var optionsText = File.ReadAllText("./settings.json");
                var options = JsonConvert.DeserializeObject<CommandLineOptions>(optionsText);
                RunOptions(options);
            }

            CommandLine.Parser.Default.ParseArguments<CommandLineOptions>(args)
              .WithParsed(RunOptions);
        }
        catch (Exception e) when (e is FileNotFoundException)
        {
            Console.WriteLine("Error:");
            Console.WriteLine(e.Message);
            Console.WriteLine();
            Console.WriteLine("Press any key to close this window . . .");
            Console.ReadLine();
        }

    }
    static void RunOptions(CommandLineOptions options)
    {
        var groupsText = File.ReadAllText(options.GroupsPath);
        var groups = JsonConvert.DeserializeObject<Dictionary<string, string>>(groupsText);
        var app = new App(options.BasePath, new TimeSpan(0, 0, 1), groups);
        app.Start(options.SkipExisting);
    }
}