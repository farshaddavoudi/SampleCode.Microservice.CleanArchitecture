using SampleMicroserviceApp.Identity.Domain.Entities.Contracts;

namespace SampleMicroserviceApp.Identity.Application.Services.SyncUsersService;

public record UserComparableRecord(
    int? RahkaranId,
    string? FirstName,
    string? LastName,
    string? FullName,
    string? NationalCode,
    int? PersonnelCode,
    string? UnitName,
    string? PostTitle,
    int? BoxId,
    int? ParentBoxId,
    int? WorkLocationCode,
    string? WorkLocation,
    string? Mobile,
    DateTime? EmployedAt,
    bool Dismissed,
    int? Gender,
    DateTime? BirthDate,
    int? MaritalStatus,
    int? EmploymentType
    )
: IUserRahkaran;