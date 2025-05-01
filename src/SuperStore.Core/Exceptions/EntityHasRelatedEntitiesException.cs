using System.Net;

namespace SuperStore.Core.Exceptions;

public sealed class EntityHasRelatedEntitiesException : ServiceApplicationException
{
    public EntityHasRelatedEntitiesException(string entityName, Guid entityId)
        : base($"Entidade {entityName} com id {entityId} possui entidades relacionadas e não pode ser deletada", HttpStatusCode.BadRequest)
    {
    }
}