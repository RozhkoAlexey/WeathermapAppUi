using Microsoft.AspNetCore.Mvc;
using WeathermapApp.DAL.Services.Interfaces;

namespace WeathermapApp.Controller;

[Route("api/[controller]")]
[ApiController]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService) =>
        _weatherService = weatherService;

    /// <summary>
    ///  Get weather data
    /// </summary>
    /// <param name="zipCode">Zip Code</param>
    /// <param name="countryCode">Country Code</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult> Get(string zipCode, string countryCode) =>
        Ok(await _weatherService.GetWeatherAsync(zipCode, countryCode));

    /// <summary>
    /// Get request history
    /// </summary>
    /// <returns></returns>
    [HttpGet(nameof(History))]
    public async Task<ActionResult> History() =>
        Ok(await _weatherService.GetHistoryAsync());

    /// <summary>
    /// Deleting an existing history by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _weatherService.RemoveAsync(id);
        
        return Ok(id);
    }
}