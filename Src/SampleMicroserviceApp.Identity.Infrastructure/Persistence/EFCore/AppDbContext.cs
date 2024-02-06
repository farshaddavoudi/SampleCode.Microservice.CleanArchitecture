using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Domain;
using SampleMicroserviceApp.Identity.Domain.Entities.Application;
using SampleMicroserviceApp.Identity.Infrastructure.Persistence.EFCore.Extensions;

namespace SampleMicroserviceApp.Identity.Infrastructure.Persistence.EFCore;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Auto Register all Entities
        modelBuilder.RegisterDbSets(typeof(DomainAssemblyEntryPoint).Assembly);

        base.OnModelCreating(modelBuilder);

        // Auto Register all Entity Configurations (Fluent-API)
        modelBuilder.ApplyConfigurations(typeof(InfrastructureAssemblyEntryPoint).Assembly);

        // EF Core Global Query Filters
        modelBuilder.RegisterIsArchivedGlobalQueryFilter();
        modelBuilder.Entity<ApplicationEntity>().HasQueryFilter(x => x.IsActive);

        modelBuilder.ConfigureDecimalPrecision();

        // Restrict Delete (in Hard delete scenarios)
        // Ef default is Cascade
        modelBuilder.SetRestrictAsDefaultDeleteBehavior();

        // Seed Base Data to Database
        modelBuilder.SeedDefaultApplication();
        modelBuilder.SeedDefaultRole();
        modelBuilder.SeedDefaultUsers();
        modelBuilder.SeedDefaultUserRoles();
        modelBuilder.SeedDefaultClaimsAndRoleClaims();
    }
}