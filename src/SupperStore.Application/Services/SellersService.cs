using SuperStore.Data.Abstractions.Repositories;
using SuperStore.Model.Entities;
using SupperStore.Application.Abstractions.Services;
using SupperStore.Application.InputModels;
using SupperStore.Application.OutputModels;

namespace SupperStore.Application.Services;
internal sealed class SellersService : ISellersService
{
    private readonly ISellersRepository _sellersRepository;

    public SellersService(ISellersRepository sellersRepository)
    {
        _sellersRepository = sellersRepository;
    }

    public async Task<SellerOutputModel> CreateAsync(CreateSellerInputModel inputModel, CancellationToken cancellationToken)
    {
        var seller = new Seller(inputModel.Name, inputModel.UserId);

        await _sellersRepository.AddAsync(seller, cancellationToken);
        await _sellersRepository.SaveChangesAsync(cancellationToken);

        return new SellerOutputModel(seller);
    }
}
