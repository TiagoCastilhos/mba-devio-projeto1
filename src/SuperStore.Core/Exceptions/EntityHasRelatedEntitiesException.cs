using System.Net;

namespace SuperStore.Core.Exceptions;

public sealed class EntityHasRelatedEntitiesException : ServiceApplicationException
{
    public EntityHasRelatedEntitiesException(string entityName, string identifier)
        : base($"{entityName} '{identifier}' possui entidades relacionadas e não pode ser deletada", HttpStatusCode.BadRequest)
    {
    }
}