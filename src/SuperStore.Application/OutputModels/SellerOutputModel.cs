using SuperStore.Model.Entities;

namespace SuperStore.Application.OutputModels;

public sealed class SellerOutputModel(Seller seller)
{
    public int Id { get; } = seller.Id;
    public string Name { get; } = seller.Name;
    public DateTimeOffset CreatedOn { get; } = seller.CreatedOn;
    public DateTimeOffset UpdatedOn { get; } = seller.UpdatedOn;
}
