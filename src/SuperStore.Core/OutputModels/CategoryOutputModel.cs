using SuperStore.Data.Entities;

namespace SuperStore.Core.OutputModels;
public sealed class CategoryOutputModel(Category category)
{
    public Guid Id { get; } = category.Id;
    public string Name { get; } = category.Name;
    public DateTime CreatedOn { get; } = category.CreatedOn;
    public DateTime UpdatedOn { get; } = category.UpdatedOn;
}
