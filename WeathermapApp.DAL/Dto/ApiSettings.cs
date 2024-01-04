namespace WeathermapApp.DAL.Dto;

public class ApiSettings
{
    /// <summary>
    /// Route for getting data
    /// </summary>
    public string? ApiRout { get; set; }

    /// <summary>
    /// Route for receiving weather icons
    /// </summary>
    public string? ImageRoute { get; set; }

    /// <summary>
    /// Authorization key
    /// </summary>
    public string? ApiKey { get; set; }
}
