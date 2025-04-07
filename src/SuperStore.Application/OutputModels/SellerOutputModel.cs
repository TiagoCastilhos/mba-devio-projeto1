using SuperStore.Model.Entities;

namespace SuperStore.Application.OutputModels;

public sealed class SellerOutputModel(Seller seller)
{
    public Guid Id { get; } = seller.Id;
    public string Name { get; } = seller.Name;
    public DateTime CreatedOn { get; } = seller.CreatedOn;
    public DateTime UpdatedOn { get; } = seller.UpdatedOn;
}
