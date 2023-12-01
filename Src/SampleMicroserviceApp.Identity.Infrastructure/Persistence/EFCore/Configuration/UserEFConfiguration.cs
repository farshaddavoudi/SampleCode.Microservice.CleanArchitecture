using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleMicroserviceApp.Identity.Domain.Entities.User;
using SampleMicroserviceApp.Identity.Infrastructure.Persistence.DbConstants;
using SampleMicroserviceApp.Identity.Infrastructure.Persistence.EFCore.Extensions;

namespace SampleMicroserviceApp.Identity.Infrastructure.Persistence.EFCore.Configuration;

public class UserEFConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable(TableNameConst.Users);
    }
}

public class UserRahkaranViewEFConfiguration : IEntityTypeConfiguration<UserRahkaranViewEntity>
{
    public void Configure(EntityTypeBuilder<UserRahkaranViewEntity> builder)
    {
        builder.ToView(ViewNameConst.UsersRahkaranView);

        builder.HasKey(x => x.RahkaranId);
    }
}

public class UserRefreshTokenHistoryEFConfiguration : IEntityTypeConfiguration<RefreshTokenHistoryEntity>
{
    public void Configure(EntityTypeBuilder<RefreshTokenHistoryEntity> builder)
    {
        builder.ToTable(TableNameConst.RefreshTokensHistory);

        builder.Property(x => x.RefreshToken).IsRequired().HasMaxLength(50);

        builder.HasUniqueIndexArchivable(x => new { x.UserId, x.CreatedAt });

        builder.HasOne(rt => rt.User)
            .WithMany(r => r.RefreshTokenHistory)
            .HasForeignKey(ur => ur.UserId);
    }
}

public class UserRoleEFConfiguration : IEntityTypeConfiguration<UserRoleEntity>
{
    public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
    {
        builder.ToTable(TableNameConst.UserRoles);

        builder.HasUniqueIndexArchivable(x => new { x.UserId, x.RoleId });

        builder.HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);
        builder.HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);
    }
}