using SampleMicroserviceApp.Identity.Domain.Entities.Contracts;

namespace SampleMicroserviceApp.Identity.Domain.Entities.User;

public class UserRahkaranViewEntity : IUserRahkaran, IEntity
{
    public int? RahkaranId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName { get; set; }
    public int? PersonnelCode { get; set; }
    public string? NationalCode { get; set; }
    public string? UnitName { get; set; }
    public string? PostTitle { get; set; }
    public int? BoxId { get; set; }
    public int? ParentBoxId { get; set; }
    public int? WorkLocationCode { get; set; }
    public string? WorkLocation { get; set; }
    public string? Mobile { get; set; }
    public DateTime? EmployedAt { get; set; }
    public bool Dismissed { get; set; }
    public int? Gender { get; set; }
    public DateTime? BirthDate { get; set; }
    public int? MaritalStatus { get; set; }
    public int? EmploymentType { get; set; }
}