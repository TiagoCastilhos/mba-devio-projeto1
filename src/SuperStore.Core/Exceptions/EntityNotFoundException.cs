using System.Net;

namespace SuperStore.Core.Exceptions;
public sealed class EntityNotFoundException : ServiceApplicationException
{
    public EntityNotFoundException(string entityName, Guid entityId)
        : base($"Entidade {entityName} com id {entityId} não foi encontrada", HttpStatusCode.NotFound)
    {
    }

    public EntityNotFoundException(string entityName, string identifier)
        : base($"Entidade {entityName} com identificador {identifier} não foi encontrada", HttpStatusCode.NotFound)
    {
    }
}
