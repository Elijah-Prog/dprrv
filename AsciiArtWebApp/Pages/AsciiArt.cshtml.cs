using Figgle;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AsciiArtWebApp.Pages;

[ResponseCache(Duration = 0,                              //#1
  Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]                                  //#2
public class AsciiArtModel : PageModel
{
    public string? RenderedText { get; set; }             //#3

    public bool ShowRenderedText =>                       //#4
      !string.IsNullOrEmpty(RenderedText);

    public AsciiArtModel() {}                             //#5

    public void OnPost(string text)                       //#6
    {
      RenderedText = FiggleFonts.Standard.Render(
        text ?? "Hello!");                                //#7
    }
}