using Microsoft.EntityFrameworkCore;
using Tokonyadia_Api.DTO;
using Tokonyadia_Api.Entities;
using Tokonyadia_Api.Middlewares;
using Tokonyadia_Api.Repositories;

namespace Tokonyadia_Api.Services;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _productRepository;
    private readonly Ipersistance _persistance;

    public ProductService(IRepository<Product> productRepository, Ipersistance persistance)
    {
        _productRepository = productRepository;
        _persistance = persistance;
    }
    
    public async Task<ProductResponse> CreateNewProduct(Product payload)
    {
        var product = await _productRepository.Find(product => product.ProductName.ToLower().Equals(payload.ProductName.ToLower()),
            new[] { "ProductPrices" });

        if (product is null)
        {
            var result = await _persistance.ExecuteTransactionAsync(async () =>
            {
                var product = await _productRepository.Save(payload);
                await _persistance.SaveChangesAsync();
                return product;
            });

            var productPriceResponses = result.ProductPrices.Select(productPrice => new ProductPriceResponse
            {
                Id = productPrice.Id.ToString(),
                Price = productPrice.Price,
                Stock = productPrice.Stock,
                StoreId = productPrice.StoreId.ToString()
            }).ToList();
        
            ProductResponse response = new()
            {
                Id = result.Id.ToString(),
                ProductName = result.ProductName,
                Description = result.Description,
                ProductPrices = productPriceResponses
            };
        
            return response;
        }

        var productPriceRequest = payload.ProductPrices.ToList();
        
        ProductPrice productPrice = new()
        {
            Price = productPriceRequest[0].Price,
            Stock = productPriceRequest[0].Stock,
            StoreId = productPriceRequest[0].StoreId
        };
        
        product.ProductPrices.Add(productPrice);
        await _persistance.SaveChangesAsync();

        ProductResponse productResponse = new()
        {
            Id = product.Id.ToString(),
            ProductName = product.ProductName,
            Description = product.Description,
            ProductPrices = new List<ProductPriceResponse>
            {
                new ()
                {
                    Id = productPrice.Id.ToString(),
                    Price = productPrice.Price,
                    Stock = productPrice.Stock,
                    StoreId = productPrice.StoreId.ToString()
                }
            }
        };

        return productResponse;
    }

    public async Task<ProductResponse> GetById(string id)
    {
        try
        {
            var product = await _productRepository.Find(product => product.Id.Equals(Guid.Parse(id)), new[] { "ProductPrices" });

            if (product is null) throw new  NotFoundException("product not found");

            var productPriceResponses = product.ProductPrices.Select(productPrice => new ProductPriceResponse
            {
                Id = productPrice.Id.ToString(),
                Price = productPrice.Price,
                Stock = productPrice.Stock,
                StoreId = productPrice.StoreId.ToString()
            }).ToList();
            
            ProductResponse response = new()
            {
                Id = product.Id.ToString(),
                ProductName = product.ProductName,
                Description = product.Description,
                ProductPrices = productPriceResponses
            };

            return response;
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task<PageResponse<ProductResponse>> GetAll(string? name, int page, int size)
    {
        var products = await _productRepository.FindAll(
            criteria: p => EF.Functions.Like(p.ProductName, $"%{name}%"),
            page: page,
            size: size,
            includes: new[] { "ProductPrices" }
        );
        
        var productResponses = products.Select(product =>
        {
            var productPriceResponses = product.ProductPrices.Select(productPrice =>
            {
                ProductPriceResponse productPriceResponse = new()
                {
                    Id = productPrice.Id.ToString(),
                    Price = productPrice.Price,
                    Stock = productPrice.Stock,
                    StoreId = productPrice.StoreId.ToString()
                };
                return productPriceResponse;
            }).ToList();

            return new ProductResponse
            {
                Id = product.Id.ToString(),
                ProductName = product.ProductName,
                Description = product.Description,
                ProductPrices = productPriceResponses
            };
        }).ToList();

        var totalPages = (int)Math.Ceiling(await _productRepository.Count() / (decimal)size);

        PageResponse<ProductResponse> pageResponse = new()
        {
            Content = productResponses,
            TotalPage = totalPages,
            TotalElement = productResponses.Count
        };

        return pageResponse;
    }

    public async Task<ProductResponse> Update(Product payload)
    {
        if (payload.Id == Guid.Empty) throw new NotFoundException("Product Not Found");
        
        _productRepository.Update(payload);
        await _persistance.SaveChangesAsync();

        // var productPriceResponses = payload.ProductPrices.Select(productPrice => new ProductPriceResponse
        // {
        //     Id = productPrice.Id.ToString(),
        //     Price = productPrice.Price,
        //     Stock = productPrice.Stock,
        //     StoreId = productPrice.StoreId.ToString()
        // }).ToList();
        
        ProductResponse response = new()
        {
            Id = payload.Id.ToString(),
            ProductName = payload.ProductName,
            Description = payload.Description,
            // ProductPrices = productPriceResponses
        };
        
        return response;
    }

    public async Task DeleteById(string id)
    {
        var product = await _productRepository.FindById(Guid.Parse(id));
        if (product is null) throw new NotFoundException("Product not found");
        _productRepository.Delete(product);
        await _persistance.SaveChangesAsync();
    }
}
