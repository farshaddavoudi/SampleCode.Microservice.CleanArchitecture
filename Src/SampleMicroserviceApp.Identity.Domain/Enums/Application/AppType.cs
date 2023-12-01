using System.ComponentModel.DataAnnotations;

namespace SampleMicroserviceApp.Identity.Domain.Enums.Application;

public enum AppType
{
    [Display(Name = "Front End")]
    FrontEnd = 10,

    [Display(Name = "Back End")]
    BackEnd = 20,

    [Display(Name = "Combined FrontEnd and BackEnd")]
    CombinedFrontAndBack = 30,

    [Display(Name = "External App")]
    ExternalApp = 40
}