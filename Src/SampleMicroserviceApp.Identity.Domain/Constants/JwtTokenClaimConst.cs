using SampleMicroserviceApp.Identity.Domain.Extensions;

namespace SampleMicroserviceApp.Identity.Domain.Constants;

public static class JwtTokenClaimConst
{
    public static string PersonnelCode = nameof(PersonnelCode).ToLowerFirstChar();
    public static string UnitName = nameof(UnitName).ToLowerFirstChar();
    public static string PostTitle = nameof(PostTitle).ToLowerFirstChar();
    public static string BoxId = nameof(BoxId).ToLowerFirstChar();
    public static string WorkLocationCode = nameof(WorkLocationCode).ToLowerFirstChar();
}