using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeathermapApp.DAL.Models;

namespace WeathermapApp.DAL.Configurations;

public abstract class BaseModelConfiguration<TModel> : IEntityTypeConfiguration<TModel> where TModel : BaseModel
{
    public virtual void Configure(EntityTypeBuilder<TModel> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(f => f.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.CreateDt)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAdd()
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save);
    }
}
