using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleMicroserviceApp.Identity.Domain.Entities.Claim;
using SampleMicroserviceApp.Identity.Infrastructure.Persistence.DbConstants;
using SampleMicroserviceApp.Identity.Infrastructure.Persistence.EFCore.Extensions;

namespace SampleMicroserviceApp.Identity.Infrastructure.Persistence.EFCore.Configuration;

public class ClaimEFConfiguration : IEntityTypeConfiguration<ClaimEntity>
{
    public void Configure(EntityTypeBuilder<ClaimEntity> builder)
    {
        builder.ToTable(TableNameConst.Claims);

        builder.Property(x => x.Key).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(300);

        builder.HasUniqueIndexArchivable(x => x.Key);

        builder.HasOne(c => c.Application)
            .WithMany(a => a.Claims)
            .HasForeignKey(c => c.ApplicationId);
    }
}