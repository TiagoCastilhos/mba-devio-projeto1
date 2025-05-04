using System.Net;

namespace SuperStore.Core.Exceptions;
public sealed class EntityNotFoundException : ServiceApplicationException
{
    public EntityNotFoundException(string entityName, Guid entityId)
        : base($"{entityName} com id '{entityId}' não foi encontrado", HttpStatusCode.NotFound)
    {
    }

    public EntityNotFoundException(string entityName, string identifier)
        : base($"{entityName} '{identifier}' não foi encontrado", HttpStatusCode.NotFound)
    {
    }
}
