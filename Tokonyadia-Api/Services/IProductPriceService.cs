using Tokonyadia_Api.Entities;

namespace Tokonyadia_Api.Services;

public interface IProductPriceService
{
    Task<ProductPrice> GetById(string id);
}