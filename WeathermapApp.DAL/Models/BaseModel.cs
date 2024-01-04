namespace WeathermapApp.DAL.Models;

public abstract class BaseModel
{
    /// <summary>
    /// Entity Id in the database
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Date and time of creation
    /// </summary>
    public DateTime CreateDt { get; set; }
}
