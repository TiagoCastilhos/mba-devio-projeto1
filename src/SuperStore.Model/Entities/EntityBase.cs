namespace SuperStore.Model.Entities;
public abstract class EntityBase
{
    public Guid Id { get; protected set; }
    public bool IsDeleted { get; protected set; }
    public DateTime CreatedOn { get; protected set; }
    public DateTime UpdatedOn { get; protected set; }

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