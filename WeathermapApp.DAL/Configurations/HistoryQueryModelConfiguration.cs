using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using WeathermapApp.DAL.Dto;
using WeathermapApp.DAL.Models;

namespace WeathermapApp.DAL.Configurations;

internal class HistoryQueryModelConfiguration : BaseModelConfiguration<HistoryQueryModel>
{
    public override void Configure(EntityTypeBuilder<HistoryQueryModel> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => new { x.ZipCode, x.CountryCode }).IsUnique(false);
        builder.Property(x => x.ZipCode)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.CountryCode)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Weather)
            .HasConversion(
                v => JsonConvert.SerializeObject(v,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<WeatherResponseDto>(v,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })!);
    }
}