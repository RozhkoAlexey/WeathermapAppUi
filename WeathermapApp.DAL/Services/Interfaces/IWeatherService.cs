using WeathermapApp.DAL.Dto;

namespace WeathermapApp.DAL.Services.Interfaces;

public interface IWeatherService
{
    /// <summary>
    /// Get weather data
    /// </summary>
    /// <param name="zipCode"></param>
    /// <param name="countryCode"></param>
    /// <returns></returns>
    Task<HistoryQueryDto?> GetWeatherAsync(string zipCode, string countryCode);

    /// <summary>
    /// Get request history
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<HistoryQueryDto?>> GetHistoryAsync();

    /// <summary>
    /// Deleting an existing history by ID  
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveAsync(Guid id);
}