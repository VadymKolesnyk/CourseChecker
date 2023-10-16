using CommandLine;
using Newtonsoft.Json;

namespace CourseChecker.ConsoleApp;
internal class CommandLineOptions
{
    [JsonProperty("path")]
    [Option('p', "path", Required = true, HelpText = @"Path to event day folder. Example: 'D:\orientir\event\test-event\D_1'.")]
    public string BasePath { get; set; }
    [JsonProperty("groups")]
    [Option('g', "groups", Default = "./groups.json", HelpText = "Path to groups json.")]
    public string GroupsPath { get; set; }
    [JsonProperty("skip-existing")]

    [Option('s', "skip-existing", HelpText = "Flag to check all records or only new.")]
    public bool SkipExisting { get; set; }
}
