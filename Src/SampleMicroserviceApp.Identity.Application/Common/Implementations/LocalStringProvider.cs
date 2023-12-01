using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.Common.Exceptions;
using SampleMicroserviceApp.Identity.Domain.Resources;
using SampleMicroserviceApp.Identity.Domain.Resources.ExceptionMessages;
using SampleMicroserviceApp.Identity.Domain.Resources.OtherMessages;
using Microsoft.Extensions.Localization;

namespace SampleMicroserviceApp.Identity.Application.Common.Implementations;

public class LocalStringProvider : ILocalStringProvider
{
    private readonly IStringLocalizer<MessageStrings> _messageStringsLocalizer;
    private readonly IStringLocalizer<ExceptionStrings> _exceptionStringsLocalizer;

    #region ctor

    public LocalStringProvider(IStringLocalizer<MessageStrings> messageStringsLocalizer, IStringLocalizer<ExceptionStrings> exceptionStringsLocalizer)
    {
        _messageStringsLocalizer = messageStringsLocalizer;
        _exceptionStringsLocalizer = exceptionStringsLocalizer;
    }

    #endregion

    /// <summary>
    /// Get value from MessageStrings resource
    /// </summary>
    /// <param name="messageStringsKey"> Resource key </param>
    /// <returns></returns>
    public string? Message(string messageStringsKey)
    {
        return GetValueByKeyAndResource(messageStringsKey, StringResourceType.MessageStrings);
    }

    /// <summary>
    /// Get value from ExceptionStrings resource
    /// </summary>
    /// <param name="exceptionStringsKey"> Resource key </param>
    /// <returns></returns>
    public string Exception(string exceptionStringsKey)
    {
        return GetValueByKeyAndResource(exceptionStringsKey, StringResourceType.ExceptionStrings) ?? string.Empty;
    }

    private string? GetValueByKeyAndResource(string key, StringResourceType stringResourceType)
    {
        if (stringResourceType == StringResourceType.MessageStrings)
            return _messageStringsLocalizer.GetString(key);

        if (stringResourceType == StringResourceType.ExceptionStrings)
            return _exceptionStringsLocalizer.GetString(key);

        throw new BadRequestException("ResourceType is invalid");
    }
}