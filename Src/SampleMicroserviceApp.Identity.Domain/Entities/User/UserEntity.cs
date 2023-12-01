using SampleMicroserviceApp.Identity.Domain.Entities.Contracts;
using SampleMicroserviceApp.Identity.Domain.Enums.User;

namespace SampleMicroserviceApp.Identity.Domain.Entities.User;

public class UserEntity : BaseEntity, IUserRahkaran
{
    public bool IsRegistered { get; set; }

    public UserSource UserSource { get; set; } = UserSource.Rahkaran;

    public string? UserName { get; set; }

    #region Rahkaran Info

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


    #endregion

    public string? HashedPassword { get; set; }

    public string? PasswordSalt { get; set; }

    public DateTime? PasswordSetAt { get; set; }

    public DateTime? LastLoggedIn { get; set; }

    public bool TwoFactorEnabled { get; set; }

    // NAVs
    public ICollection<UserRoleEntity> UserRoles { get; set; } = new List<UserRoleEntity>();
    public ICollection<RefreshTokenHistoryEntity> RefreshTokenHistory { get; set; } = new List<RefreshTokenHistoryEntity>();
}