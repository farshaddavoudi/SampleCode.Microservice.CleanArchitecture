using AutoMapper;
using SampleMicroserviceApp.Identity.Application.CQRS.Application.DTOs;
using SampleMicroserviceApp.Identity.Application.CQRS.Role.Commands.CreateOrUpdateRole;
using SampleMicroserviceApp.Identity.Domain.Entities.Role;

namespace SampleMicroserviceApp.Identity.Application.Common.Mappings;

public class RoleMapperConfiguration : Profile
{
    public RoleMapperConfiguration()
    {
        CreateMap<CreateOrUpdateRoleCommand, RoleEntity>();

        CreateMap<RoleEntity, RoleDto>();
    }
}