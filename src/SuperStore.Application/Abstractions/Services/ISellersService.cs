using SuperStore.Application.InputModels;
using SuperStore.Application.OutputModels;

namespace SuperStore.Application.Abstractions.Services;
public interface ISellersService
{
    Task<SellerOutputModel> CreateAsync(CreateSellerInputModel inputModel, CancellationToken cancellationToken);
}
