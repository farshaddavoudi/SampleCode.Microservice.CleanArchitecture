namespace SampleMicroserviceApp.Identity.Domain.Entities.Contracts;

/// <summary>
/// The purpose of this interface is to be sure a new property in Rahkaran will also be implemented to main user entity and Comparable user record type
/// </summary>
public interface IUserRahkaran
{
    // Do not put <init> or <set> in { get; set/int; } because both record and class types want to implement it
    public int? RahkaranId { get; }
    public string? FirstName { get; }
    public string? LastName { get; }
    public string? FullName { get; }
    public int? PersonnelCode { get; }
    public string? NationalCode { get; }
    public string? UnitName { get; }
    public string? PostTitle { get; }
    public int? BoxId { get; }
    public int? ParentBoxId { get; }
    public int? WorkLocationCode { get; }
    public string? WorkLocation { get; }
    public string? Mobile { get; }
    public DateTime? EmployedAt { get; }
    public bool Dismissed { get; }
    public int? Gender { get; }
    public DateTime? BirthDate { get; }
    public int? MaritalStatus { get; }
    public int? EmploymentType { get; }
}