using Microsoft.EntityFrameworkCore;
using SampleMicroserviceApp.Identity.Domain.Constants;
using SampleMicroserviceApp.Identity.Domain.Entities.Application;
using SampleMicroserviceApp.Identity.Domain.Entities.Claim;
using SampleMicroserviceApp.Identity.Domain.Entities.Role;
using SampleMicroserviceApp.Identity.Domain.Entities.User;
using SampleMicroserviceApp.Identity.Domain.Enums.Application;
using SampleMicroserviceApp.Identity.Domain.Enums.User;
using SampleMicroserviceApp.Identity.Domain.Shared;

namespace SampleMicroserviceApp.Identity.Infrastructure.Persistence.EFCore;

public static class DataSeeder
{
    public static ModelBuilder SeedDefaultApplication(this ModelBuilder modelBuilder)
    {
        ApplicationEntity[] apps =
        {
            new()
            {
                Id = 1,
                CreatedAt = new DateTime(2023, 1, 1),
                IsArchived = false,
                Key = AppMetadataConst.AppKey,
                Title = AppMetadataConst.PersianFullName,
                AppType = AppType.BackEnd,
                IsPublic = true,
                IsActive = true,
                Description = "Identity service which contains apps, users, roles, claims and handles permissions"
            }
        };

        modelBuilder.Entity<ApplicationEntity>().HasData(apps);

        return modelBuilder;
    }

    public static ModelBuilder SeedDefaultRole(this ModelBuilder modelBuilder)
    {
        RoleEntity[] administratorRole =
        {
            new()
            {
                Id = 1,
                CreatedAt = new DateTime(2023, 1, 1),
                IsArchived = false,
                ApplicationId = 1,
                Key = RoleConst.Identity_Administrator,
                Title = "Identity Administrator",
            }
        };

        modelBuilder.Entity<RoleEntity>().HasData(administratorRole);

        return modelBuilder;
    }

    public static ModelBuilder SeedDefaultUsers(this ModelBuilder modelBuilder)
    {
        UserEntity[] users =
        {
            new()
            {
                Id = 1,
                CreatedAt = new DateTime(2023, 1, 1),
                IsArchived = false,
                IsRegistered = false,
                UserSource = UserSource.Manual,
                UserName = null,
                FirstName = "رامین", //fa
                LastName = "یزدانی",
                FullName = "رامین یزدانی",
                Mobile = "09195159945"
            },
            new()
            {
                Id = 2,
                CreatedAt = new DateTime(2023, 1, 1),
                IsArchived = false,
                IsRegistered = false,
                UserSource = UserSource.Manual,
                UserName = null,
                FirstName = "فرشاد", //fa
                LastName = "داودی",
                FullName = "فرشاد داودی",
                Mobile = "0119029198"
            },
        };

        modelBuilder.Entity<UserEntity>().HasData(users);

        return modelBuilder;
    }

    public static ModelBuilder SeedDefaultUserRoles(this ModelBuilder modelBuilder)
    {
        UserRoleEntity[] userRoles =
        {
            new()
            {
                Id = 1,
                CreatedAt = new DateTime(2023, 1, 1),
                IsArchived = false,
                UserId = 1,
                RoleId = 1
            },
            new()
            {
                Id = 2,
                CreatedAt = new DateTime(2023, 1, 1),
                IsArchived = false,
                UserId = 2,
                RoleId = 1
            }
        };

        modelBuilder.Entity<UserRoleEntity>().HasData(userRoles);

        return modelBuilder;
    }

    public static ModelBuilder SeedDefaultClaimsAndRoleClaims(this ModelBuilder modelBuilder)
    {
        var allAppClaims = ClaimConst.GetAllAppClaims();

        int claimId = 1;

        List<ClaimEntity> claims = new();

        List<RoleClaimEntity> roleClaims = new();

        foreach (var appClaim in allAppClaims)
        {
            claims.Add(new ClaimEntity
            {
                Id = claimId,
                ApplicationId = 1,
                Key = appClaim,
                IsArchived = false,
                Title = appClaim,
                CreatedAt = new DateTime(2023, 1, 1)
            });

            roleClaims.Add(new RoleClaimEntity
            {
                Id = claimId,
                CreatedAt = new DateTime(2023, 1, 1),
                IsArchived = false,
                ClaimId = claimId,
                RoleId = 1
            });

            claimId++;
        }

        modelBuilder.Entity<ClaimEntity>().HasData(claims);

        modelBuilder.Entity<RoleClaimEntity>().HasData(roleClaims);

        return modelBuilder;
    }
}