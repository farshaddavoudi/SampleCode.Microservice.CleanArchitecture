using AutoMapper;
using SampleMicroserviceApp.Identity.Application.CQRS.Application.Commands.CreateOrUpdateApplication;
using SampleMicroserviceApp.Identity.Application.CQRS.Application.DTOs;
using SampleMicroserviceApp.Identity.Domain.Entities.Application;
using SampleMicroserviceApp.Identity.Domain.Extensions;

namespace SampleMicroserviceApp.Identity.Application.Common.Mappings;

public class ApplicationMapperConfiguration : Profile
{
    public ApplicationMapperConfiguration()
    {
        CreateMap<CreateOrUpdateApplicationCommand, ApplicationEntity>();

        CreateMap<ApplicationEntity, ApplicationDto>()
            .ForMember(dto => dto.AppTypeDisplay, config =>
                config.MapFrom(e => e.AppType.ToDisplayName(true)));
    }
}