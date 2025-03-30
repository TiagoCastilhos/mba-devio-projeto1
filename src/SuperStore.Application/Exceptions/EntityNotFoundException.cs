namespace SuperStore.Application.Exceptions;

public sealed class EntityNotFoundException : ApplicationException
{
    public EntityNotFoundException(string entityName, int entityId)
        : base($"Entidade {entityName} com id {entityId} não foi encontrada")
    {
    }
}