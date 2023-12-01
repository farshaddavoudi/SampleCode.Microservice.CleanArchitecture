namespace SampleMicroserviceApp.Identity.Domain.Entities.Contracts;

public interface IBaseEntity<TKey> : IArchivableEntity //, IAuditableEntity
{
    TKey Id { get; set; }

    DateTime CreatedAt { get; set; }

    DateTime? ModifiedAt { get; set; }
}

public interface IBaseEntity : IBaseEntity<int>
{

}