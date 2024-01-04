namespace WeathermapApp.DAL.Dto;

public record WeatherDto
{
    /// <summary>
    /// Weather condition id
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Group of weather parameters (Rain, Snow, Extreme etc.)
    /// </summary>
    public string? Main { get; init; }

    /// <summary>
    /// Weather condition within the group. You can get the output in your language
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Weather icon id
    /// </summary>
    public string? Icon { get; set; }
}