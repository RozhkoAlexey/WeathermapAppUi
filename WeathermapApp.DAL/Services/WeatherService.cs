using System.Net.Http.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using WeathermapApp.DAL.Dto;
using WeathermapApp.DAL.Models;
using WeathermapApp.DAL.Repositories.Interfaces;
using WeathermapApp.DAL.Services.Interfaces;

namespace WeathermapApp.DAL.Services;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _memoryCache;
    private readonly IHistoryQueryRepository _repository;
    private readonly string? _apiKey;
    private readonly string? _imageRoute;
    private readonly IMapper _mapper;

    public WeatherService(HttpClient httpClient, 
        IOptions<ApiSettings> options, 
        IMemoryCache memoryCache, 
        IHistoryQueryRepository repository,
        IMapper mapper)
    {
        _httpClient = httpClient;
        _memoryCache = memoryCache;
        _repository = repository;
        _apiKey = options.Value.ApiKey;
        _imageRoute = options.Value.ImageRoute;
        _mapper = mapper;

        if (options.Value.ApiRout == null)
        {
            throw new NullReferenceException("API address not specified");
        }
        
        _httpClient.BaseAddress = new Uri(options.Value.ApiRout);
    }

    /// <summary>
    /// Get weather data
    /// </summary>
    /// <param name="zipCode"></param>
    /// <param name="countryCode"></param>
    /// <returns></returns>
    public async Task<HistoryQueryDto?> GetWeatherAsync(string zipCode, string countryCode)
    {
        var data = (await _httpClient
            .GetFromJsonAsync<WeatherResponseDto>($"weather?zip={zipCode},{countryCode}&appid={_apiKey}&units=metric")) ?? new WeatherResponseDto();

        var history = new HistoryQueryModel()
        {
            ZipCode = zipCode,
            CountryCode = countryCode,
        };

        if (data.Weather is null)
        {
            history.Weather = new WeatherResponseDto();
            await _repository.CreateAsync(history);

            return _mapper.Map<HistoryQueryDto?>(history);
        }

        foreach (var item in data.Weather)
        {
            item.Icon = await GetIconAsync(item);
        }

        var historyWithData = new HistoryQueryModel()
        {
            ZipCode = zipCode,
            CountryCode = countryCode,
            Weather = data
        };

        await _repository.CreateAsync(historyWithData);

        return _mapper.Map<HistoryQueryDto?>(historyWithData);
    }

    /// <summary>
    /// Get request history
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<HistoryQueryDto?>> GetHistoryAsync()
    {
        var histories = await _repository.GetQuery()
            .AsNoTracking()
            .Where(x => x.Weather != null)
            .OrderByDescending(x => x.CreateDt)
            .ToArrayAsync();

        return _mapper.Map<IEnumerable<HistoryQueryDto>>(histories);
    }

    /// <summary>
    /// Deleting an existing history by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task RemoveAsync(Guid id) =>
        await _repository.RemoveAsync(id);

    /// <summary>
    /// Convert weather icon to string
    /// </summary>
    /// <param name="itemIcon"></param>
    /// <returns></returns>
    private async Task<string> GetBase64ImageAsync(string itemIcon)
    {
        using var response = await _httpClient.GetAsync($"{_imageRoute}/{itemIcon}@2x.png");

        response.EnsureSuccessStatusCode();

        var imageBytes = await response.Content.ReadAsByteArrayAsync();
        
        return Convert.ToBase64String(imageBytes);
    }

    private async Task<string?> GetIconAsync(WeatherDto weather)
    {
        if (weather.Icon is null)
            return null;

        if (_memoryCache.TryGetValue(weather.Icon, out var value) && value is not null)
            return (string)value;

        var imageBase64 = await GetBase64ImageAsync(weather.Icon);

        _memoryCache.Set(weather.Icon, imageBase64);

        return imageBase64;
    }
}
