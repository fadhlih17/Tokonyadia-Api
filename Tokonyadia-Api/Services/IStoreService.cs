using Tokonyadia_Api.DTO;
using Tokonyadia_Api.Entities;

namespace Tokonyadia_Api.Services;

public interface IStoreService
{
    Task<StoreResponse> CreateNewStore(Store payload);
    Task<StoreResponse> GetById(string id);
    Task<PageResponse<StoreResponse>> GetAll(string? name, int page, int size);
    Task<StoreResponse> Update(Store payload);
    Task DeleteById(string id);
}