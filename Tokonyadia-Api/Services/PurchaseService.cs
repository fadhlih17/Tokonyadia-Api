using Microsoft.EntityFrameworkCore;
using Tokonyadia_Api.DTO;
using Tokonyadia_Api.Entities;
using Tokonyadia_Api.Middlewares;
using Tokonyadia_Api.Repositories;

namespace Tokonyadia_Api.Services;

public class PurchaseService : IPurchaseService
{
    private readonly IRepository<Purchase> _purchaseRepository;
    private readonly Ipersistance _persistance;

    public PurchaseService(IRepository<Purchase> purchaseRepository, Ipersistance persistance)
    {
        _purchaseRepository = purchaseRepository;
        _persistance = persistance;
    }
    
    public async Task<PurchaseResponse> CreateNewPurchase(Purchase payload)
    {
        var result = await _persistance.ExecuteTransactionAsync(async () =>
        {
            payload.TransDate = DateTime.Now;
            var purchase = await _purchaseRepository.Save(payload);
            await _persistance.SaveChangesAsync();
            return purchase;
        });

        var purchaseDetailResponses = result.PurchaseDetails.Select(purchaseDetail => new PurchaseDetailResponse
        {
            Id = purchaseDetail.Id.ToString(),
            Qty = purchaseDetail.Qty,
            PurchaseId = purchaseDetail.PurchaseId.ToString(),
            ProductPriceId = purchaseDetail.ProductPriceId.ToString()
        }).ToList();
        
        PurchaseResponse response = new()
        {
            Id = result.Id.ToString(),
            TransDate = result.TransDate,
            CustomerId = result.CustomerId.ToString(),
            PurchaseDetails = purchaseDetailResponses,
        };
        
        return response;
    }

    public async Task<PurchaseResponse> GetById(string id)
    {
        var purchase = await _purchaseRepository.Find(purchase => purchase.Id.Equals(Guid.Parse(id)),
            new []{ "PurchaseDetails" });

        if (purchase is null) throw new NotFoundException("Purchase not found");

        var purchaseDetailResponse = purchase.PurchaseDetails.Select(purchaseDetail => new PurchaseDetailResponse
        {
            Id = purchaseDetail.Id.ToString(),
            Qty = purchaseDetail.Qty,
            PurchaseId = purchaseDetail.PurchaseId.ToString(),
            ProductPriceId = purchaseDetail.ProductPriceId.ToString()
        }).ToList();

        PurchaseResponse response = new PurchaseResponse
        {
            Id = purchase.Id.ToString(),
            TransDate = purchase.TransDate,
            CustomerId = purchase.CustomerId.ToString(),
            PurchaseDetails = purchaseDetailResponse
        };

        return response;
    }

    public async Task<PageResponse<PurchaseResponse>> GetAll(int page, int size)
    {
        var purchases = await _purchaseRepository.FindAll(
            criteria: purchase => true, 
            page: page,
            size: size,
            includes: new[] { "PurchaseDetails" }
        );
        
        var purchaseResponses = purchases.Select(purchase =>
        {
            var purchaseDetailResponses = purchase.PurchaseDetails.Select(purchaseDetail =>
            {
                PurchaseDetailResponse purchaseDetailResponse = new()
                {
                    Id = purchaseDetail.Id.ToString(),
                    Qty = purchaseDetail.Qty,
                    PurchaseId = purchaseDetail.PurchaseId.ToString(),
                    ProductPriceId = purchaseDetail.ProductPriceId.ToString()
                };
                return purchaseDetailResponse;
            }).ToList();

            return new PurchaseResponse
            {
                Id = purchase.Id.ToString(),
                TransDate = purchase.TransDate,
                CustomerId = purchase.CustomerId.ToString(),
                PurchaseDetails = purchaseDetailResponses
            };
        }).ToList();

        var totalPages = (int)Math.Ceiling(await _purchaseRepository.Count() / (decimal)size);

        PageResponse<PurchaseResponse> pageResponse = new()
        {
            Content = purchaseResponses,
            TotalPage = totalPages,
            TotalElement = purchaseResponses.Count
        };

        if (pageResponse.TotalElement == 0) throw new NotFoundException("Data not found");
        
        return pageResponse;
    }
}