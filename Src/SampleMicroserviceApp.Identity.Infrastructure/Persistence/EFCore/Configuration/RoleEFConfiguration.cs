using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleMicroserviceApp.Identity.Domain.Entities.Role;
using SampleMicroserviceApp.Identity.Infrastructure.Persistence.DbConstants;
using SampleMicroserviceApp.Identity.Infrastructure.Persistence.EFCore.Extensions;

namespace SampleMicroserviceApp.Identity.Infrastructure.Persistence.EFCore.Configuration;

public class RoleEFConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.ToTable(TableNameConst.Roles);

        builder.Property(x => x.Key).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(300);

        builder.HasUniqueIndexArchivable(x => x.Key);

        builder.HasOne(c => c.Application)
            .WithMany(a => a.Roles)
            .HasForeignKey(c => c.ApplicationId);
    }
}

public class RoleClaimEFConfiguration : IEntityTypeConfiguration<RoleClaimEntity>
{
    public void Configure(EntityTypeBuilder<RoleClaimEntity> builder)
    {
        builder.ToTable(TableNameConst.RoleClaims);

        builder.HasUniqueIndexArchivable(x => new { x.RoleId, x.ClaimId });

        builder.HasOne(rc => rc.Role)
            .WithMany(r => r.RoleClaims)
            .HasForeignKey(rc => rc.RoleId);
        builder.HasOne(rc => rc.Claim)
            .WithMany(r => r.RoleClaims)
            .HasForeignKey(rc => rc.ClaimId);
    }
}