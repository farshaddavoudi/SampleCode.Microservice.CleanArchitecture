using System.ComponentModel.DataAnnotations;

namespace SampleMicroserviceApp.Identity.Domain.Enums.User;

public enum MaritalStatus
{
    [Display(Name = "مجرد")]
    Bachelor = 1,

    [Display(Name = "متاهل")]
    Married = 2
}