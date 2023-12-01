using AutoMapper;
using SampleMicroserviceApp.Identity.Application.CQRS.User.DTOs;
using SampleMicroserviceApp.Identity.Application.Services.SyncUsersService;
using SampleMicroserviceApp.Identity.Domain.Entities.User;

namespace SampleMicroserviceApp.Identity.Application.Common.Mappings;

public class UserMapperConfiguration : Profile
{
    public UserMapperConfiguration()
    {
        CreateMap<UserEntity, UserMiniDto>();

        CreateMap<UserComparableRecord, UserMiniDto>();

        CreateMap<UserComparableRecord, UserEntity>();

        CreateMap<UserRahkaranViewEntity, UserComparableRecord>();

        CreateMap<UserEntity, UserComparableRecord>();
    }
}