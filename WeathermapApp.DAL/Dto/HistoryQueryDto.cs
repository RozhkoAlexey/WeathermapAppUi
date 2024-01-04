namespace WeathermapApp.DAL.Dto;

public record HistoryQueryDto
{
    /// <summary>
    /// Entity Id in the database
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Date and time of creation
    /// </summary>
    public DateTime CreateDt { get; init; }

    /// <summary>
    /// More weather information
    /// </summary>
    public WeatherResponseDto? Weather { get; init; }
}
