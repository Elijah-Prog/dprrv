using CommandLine;                                        //#1

namespace HelloDotNet;                                    //#2

public record Options                                     //#3
{
  [Value(0, Required = true)]                             //#4
  public string? Text { get; init; }                      //#5

  [Option('f', "font")]                                   //#6
  public string? Font { get; init; }                      //#7
}