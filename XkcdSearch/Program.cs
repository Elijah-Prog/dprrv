using XkcdSearch;
using CommandLine;
using CommandLine.Text;
using System.Diagnostics;


var results = Parser.Default.ParseArguments<Options>(args)
    .WithParsed<Options>(options =>
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var comic = GetComicWithTitle(options.Title!);
        stopwatch.Stop();
        WriteLine("Result returned in " +
        $"{stopwatch.ElapsedMilliseconds} ms");
        if(comic == null){
            WriteLine($"xkcd comic with title " +
            $"\"{options.Title}\" not found");
        }
        else
        {
            WriteLine($"xkcd \"{options.Title}\" is number " +
            $"{comic.Number} published on {comic.Date}");
        }
    });

results.WithNotParsed(_ =>
    WriteLine(HelpText.RenderUsageText(results))
);

// Method to find a xkcd comic by its title

static Comic? GetComicWithTitle(string title)
{
    var lastComic = Comic.GetComic(0);
    for (int number = lastComic!.Number;
         number > 0;
         number--)
    {
        var comic = Comic.GetComic(number);
        if (comic != null &&
          string.Equals(title, comic!.Title,
          StringComparison.OrdinalIgnoreCase))
        {
            return comic;
        }
    }

    return null;
}