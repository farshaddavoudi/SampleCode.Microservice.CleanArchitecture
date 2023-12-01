using System.Security.Principal;

namespace SampleMicroserviceApp.Identity.Domain.Entities.Contracts;

public interface IArchivableEntity : IEntity
{
    bool IsArchived { get; set; }
}