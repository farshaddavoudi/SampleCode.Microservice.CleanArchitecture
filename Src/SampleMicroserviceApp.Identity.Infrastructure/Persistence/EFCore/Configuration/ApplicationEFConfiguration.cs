using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using SampleMicroserviceApp.Identity.Domain.Entities.Application;
using SampleMicroserviceApp.Identity.Infrastructure.Persistence.DbConstants;
using SampleMicroserviceApp.Identity.Infrastructure.Persistence.EFCore.Extensions;

namespace SampleMicroserviceApp.Identity.Infrastructure.Persistence.EFCore.Configuration;

public class ApplicationEFConfiguration : IEntityTypeConfiguration<ApplicationEntity>
{
    public void Configure(EntityTypeBuilder<ApplicationEntity> builder)
    {
        builder.ToTable(TableNameConst.Applications);

        builder.Property(x => x.Key).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(300);
        builder.Property(x => x.IsActive).HasDefaultValue(true);
        builder.Property(x => x.RelatedApps)
            .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                v => JsonSerializer.Deserialize<List<int>>(v, new JsonSerializerOptions()));

        builder.HasUniqueIndexArchivable(x => x.Key);
    }
}