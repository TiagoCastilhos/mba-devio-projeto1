using SuperStore.Model.Entities;

namespace SuperStore.Application.OutputModels;
public sealed class CategoryOutputModel(Category category)
{
    public int Id { get; } = category.Id;
    public string Name { get; } = category.Name;
    public DateTimeOffset CreatedOn { get; } = category.CreatedOn;
    public DateTimeOffset UpdatedOn { get; } = category.UpdatedOn;
}
