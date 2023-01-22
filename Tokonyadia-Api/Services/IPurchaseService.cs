using Tokonyadia_Api.DTO;
using Tokonyadia_Api.Entities;

namespace Tokonyadia_Api.Services;

public interface IPurchaseService
{
    Task<PurchaseResponse> CreateNewPurchase(Purchase payload);
    Task<PurchaseResponse> GetById(string id);
    Task<PageResponse<PurchaseResponse>> GetAll(int page, int size);
}