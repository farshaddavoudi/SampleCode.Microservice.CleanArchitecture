namespace SampleMicroserviceApp.Identity.Domain.Entities.User;

public class UserOpsViewEntity
{
    public string? CrewCode { get; set; }
    public string? ScheduleName { get; set; }
    public string? ENFirstName { get; set; }
    public string? ENLastName { get; set; }
    public string? Pos { get; set; }
    public string? ACType { get; set; }
    public string? MultiType { get; set; }
    public string? JobPosition { get; set; }
    public string? PassportNo { get; set; }
    public string? LicenceNo { get; set; }
    public string? BaseStation { get; set; }
    public int? PassportExpireDaysLeft { get; set; }
    public int? MedicalExpireDaysLeft { get; set; }
    public int? LicenceExpireDaysLeft { get; set; }
    public int? CMCExpireDaysLeft { get; set; }
    public int? L4ExpireLeft { get; set; }
    public DateTime? PassportExpire { get; set; }
    public DateTime? MedicalExpire { get; set; }
    public DateTime? LicenceExpire { get; set; }
    public DateTime? CMCExpire { get; set; }
    public DateTime? L4Expire { get; set; }
}