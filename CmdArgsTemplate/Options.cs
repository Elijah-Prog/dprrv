using CommandLine;

namespace CmdArgsTemplate;

public record Options
{
    [Value(0, Required = true , HelpText = "Some text")]
    public string? Text { get; init;}
}