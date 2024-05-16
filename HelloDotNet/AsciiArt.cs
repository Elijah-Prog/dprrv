using System.Reflection;                                  //#1

namespace HelloDotNet;

public static class AsciiArt
{
  public static void Write(Options o)                     //#2
  {
    FiggleFont? font = null;                              //#3
    if (!string.IsNullOrWhiteSpace(o.Font))               //#4
    {
      font = typeof(FiggleFonts)
        .GetProperty(o.Font,                              //#5
          BindingFlags.Static | BindingFlags.Public)      //#6
        ?.GetValue(null)                                  //#7
        as FiggleFont;                                    //#8
      if (font == null)                                   //#9
      {
        WriteLine($"Could not find font '{o.Font}'");     //#10
      }
    }

    font ??= FiggleFonts.Standard;                        //#11

    if (o?.Text != null)
    {
      WriteLine(font.Render(o.Text));                     //#12
      WriteLine($"Brought to you by {typeof(AsciiArt).FullName}");
    }
  }

}