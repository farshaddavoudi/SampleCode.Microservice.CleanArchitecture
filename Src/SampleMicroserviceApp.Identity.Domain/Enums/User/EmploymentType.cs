using System.ComponentModel.DataAnnotations;

namespace SampleMicroserviceApp.Identity.Domain.Enums.User;

public enum EmploymentType
{
    [Display(Name = "قراردادی")]
    Contractual = 1,

    [Display(Name = "قراردادی – بازنشسته")]
    ContractualRetired = 2,

    [Display(Name = "مشاوره‌ای")]
    Consulting = 3,

    [Display(Name = "ساعتی")]
    Hourly = 4
}