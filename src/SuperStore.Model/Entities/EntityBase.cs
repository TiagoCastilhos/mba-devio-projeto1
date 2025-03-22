namespace SuperStore.Model.Entities;
public abstract class EntityBase
{
    public int Id { get; protected set; }
    public bool IsDeleted { get; protected set; }
    public DateTimeOffset CreatedOn { get; protected set; }
    public DateTimeOffset UpdatedOn { get; protected set; }

    public override bool Equals(object obj)
    {
        if (obj == null || obj is not EntityBase entityToCompare)
            return false;

        if (ReferenceEquals(this, entityToCompare)) 
            return true;

        return Id == entityToCompare.Id;
    }

    public override int GetHashCode()
    {
        return (GetType().GetHashCode() * 368) + Id.GetHashCode();
    }
}