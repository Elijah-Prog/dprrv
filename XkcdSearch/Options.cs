using CommandLine;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XkcdSearch;
//Comic record in XkcdSearch application that can be used for deserializing xkcd JSON metadata
public record Options
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
}