namespace WeathermapApp.DAL.Dto;

public record WeatherResponseDto
{
    /// <summary>
    /// City ID
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// City Name
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Temperature Information
    /// </summary>
    public WeatherMainDto? Main { get; init; }

    /// <summary>
    /// Weather
    /// </summary>
    public IEnumerable<WeatherDto>? Weather { get; init; }
}
