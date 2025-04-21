using SuperStore.Core.InputModels;
using SuperStore.Core.OutputModels;

namespace SuperStore.Core.Abstractions.Services;
public interface ISellersService
{
    Task<SellerOutputModel> CreateAsync(CreateSellerInputModel inputModel, CancellationToken cancellationToken);
}
