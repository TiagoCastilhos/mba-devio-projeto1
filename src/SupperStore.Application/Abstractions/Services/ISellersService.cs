using SupperStore.Application.InputModels;
using SupperStore.Application.OutputModels;

namespace SupperStore.Application.Abstractions.Services;
public interface ISellersService
{
    Task<SellerOutputModel> CreateAsync(CreateSellerInputModel inputModel, CancellationToken cancellationToken);
}
