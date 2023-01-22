using Microsoft.EntityFrameworkCore;
using Tokonyadia_Api.DTO;
using Tokonyadia_Api.Entities;
using Tokonyadia_Api.Repositories;

namespace Tokonyadia_Api.Services;

public class StoreService : IStoreService
{
    private IRepository<Store> _storeRepository;
    private Ipersistance _persistance;

    public StoreService(IRepository<Store> storeRepository, Ipersistance persistance)
    {
        _storeRepository = storeRepository;
        _persistance = persistance;
    }
    
    public async Task<StoreResponse> CreateNewStore(Store payload)
    {
        var result = await _persistance.ExecuteTransactionAsync(async () =>
        {
            var store = await _storeRepository.Save(payload);
            await _persistance.SaveChangesAsync();
            return store;
        });

        StoreResponse response = new StoreResponse
        {
            Id = result.Id.ToString(),
            StoreName = result.StoreName,
            SiupNumber = result.SiupNumber,
            Address = result.Address,
            PhoneNumber = result.PhoneNumber
        };

        return response;
    }

    public async Task<StoreResponse> GetById(string id)
    {
        var store = await _storeRepository.FindById(Guid.Parse(id));

        if (store is null) throw new Exception("Store Not Found");
        
        StoreResponse response = new StoreResponse
        {
            Id = store.Id.ToString(),
            StoreName = store.StoreName,
            SiupNumber = store.SiupNumber,
            Address = store.Address,
            PhoneNumber = store.PhoneNumber
        };

        return response;
    }

    public async Task<PageResponse<StoreResponse>> GetAll(string? name, int page, int size)
    {
        var stores = await _storeRepository.FindAll(
            criteria: p => EF.Functions.Like(p.StoreName, $"%{name}%"),
            page: page,
            size: size
        );

        var storeResponses = stores.Select(store =>
        {
            return new StoreResponse
            {
                Id = store.Id.ToString(),
                StoreName = store.StoreName,
                SiupNumber = store.SiupNumber,
                Address = store.Address,
                PhoneNumber = store.PhoneNumber
            };
        }).ToList();

        var totalPages = (int)Math.Ceiling(await _storeRepository.Count() / (decimal)size);

        PageResponse<StoreResponse> pageResponse = new()
        {
            Content = storeResponses,
            TotalPage = totalPages,
            TotalElement = storeResponses.Count
        };

        return pageResponse;
    }

    public async Task<StoreResponse> Update(Store payload)
    {
        if (payload.Id == Guid.Empty) throw new Exception("Store Not Found");

        _storeRepository.Update(payload);
        await _persistance.SaveChangesAsync();

        StoreResponse storeResponse = new()
        {
            Id = payload.Id.ToString(),
            StoreName = payload.StoreName,
            SiupNumber = payload.SiupNumber,
            Address = payload.Address,
            PhoneNumber = payload.PhoneNumber
        };

        return storeResponse;
    }

    public async Task DeleteById(string id)
    {
        var store = await _storeRepository.FindById(Guid.Parse(id));
        if (store is null) throw new Exception("Store not found");
        _storeRepository.Delete(store);
        await _persistance.SaveChangesAsync();
    }
}