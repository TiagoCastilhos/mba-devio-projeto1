using SuperStore.Data.Abstractions.Repositories;
using SuperStore.Model.Entities;
using SuperStore.Application.Abstractions.Services;
using SuperStore.Application.InputModels;
using SuperStore.Application.OutputModels;

namespace SuperStore.Application.Services;
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
