using WeathermapApp.DAL.Dto;

namespace WeathermapApp.DAL.Models;

public class HistoryQueryModel : BaseModel
{
    public string ZipCode { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public WeatherResponseDto? Weather { get; set; }
}
