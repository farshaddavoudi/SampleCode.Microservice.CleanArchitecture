using System.Reflection;

namespace SampleMicroserviceApp.Identity.Domain.Shared;

public static class AppMetadataConst
{
    public static readonly string AppVersion = Assembly.GetExecutingAssembly().GetName().Version!.ToString(3);

    public static readonly string AppKey = "Identity";

    public static readonly string PersianFullName = "احراز هویت";

    public static readonly string SolutionName = "SampleMicroserviceApp.Identity";
}