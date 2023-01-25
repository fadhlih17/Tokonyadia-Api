using Tokonyadia_Api.Entities;
using Tokonyadia_Api.Middlewares;
using Tokonyadia_Api.Repositories;

namespace Tokonyadia_Api.Services;

public class ProductPriceService : IProductPriceService
{
    private readonly IRepository<ProductPrice> _repository;

    public ProductPriceService(IRepository<ProductPrice> repository)
    {
        _repository = repository;
    }

    public async Task<ProductPrice> GetById(string id)
    {
        try
        {
            var productPrice = await _repository.Find(price => price.Id.Equals(Guid.Parse(id)), new []{"Product"});
            if (productPrice is null) throw new NotFoundException("Product Price Not Found");
            return productPrice;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}