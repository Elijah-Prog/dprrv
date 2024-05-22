using System.Text.Json;
using System.Text.Json.Serialization;

namespace XkcdSearch;

public record Comic
{
  [JsonPropertyName("num")]
  public int Number { get; init; }
  [JsonPropertyName("safe_title")]
  public string? Title { get; init; }
  [JsonPropertyName("month")]
  public string? Month { get; init; }
  [JsonPropertyName("day")]
  public string? Day { get; init; }
  [JsonPropertyName("year")]
  public string? Year { get; init; }
  [JsonIgnore]
  public DateOnly Date =>
    DateOnly.Parse($"{Year}-{Month}-{Day}");

  // Method in Comic record that retrieves a comic via HTTP request and de-serializes it to a Comic object
  private static HttpClient client = new HttpClient()
  { BaseAddress = new Uri("https://xkcd.com") };

  public static Comic? GetComic(int number)
  {
    try
    {
      var path = number == 0 ? "info.0.json"
        : $"{number}/info.0.json";
      var stream = client.GetStreamAsync(path).Result;

      return JsonSerializer.Deserialize<Comic>(stream);
    }
    catch (AggregateException e)
      when (e.InnerException is HttpRequestException)
    {
      return null;
    }
    catch (HttpRequestException)
    {
      return null;
    }
  }
  
}
