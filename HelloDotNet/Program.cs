using CommandLine;
using HelloDotNet;

Parser.Default.ParseArguments<Options>(args)
.WithParsed(AsciiArt.Write).WithNotParsed(errors =>
{
  foreach (var error in errors)
  {
    WriteLine(error);
  }
});