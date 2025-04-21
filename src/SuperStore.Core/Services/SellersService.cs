using SuperStore.Data.Abstractions.Repositories;
using SuperStore.Data.Entities;
using SuperStore.Core.Abstractions.Services;
using SuperStore.Core.InputModels;
using SuperStore.Core.OutputModels;

namespace SuperStore.Core.Services;
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
