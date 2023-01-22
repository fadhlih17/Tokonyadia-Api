using System.Net;
using Microsoft.AspNetCore.Mvc;
using Tokonyadia_Api.DTO;
using Tokonyadia_Api.Entities;
using Tokonyadia_Api.Repositories;
using Tokonyadia_Api.Services;

namespace Tokonyadia_Api.Controllers;

[ApiController]
[Route("api/purchases")]
public class PurchaseController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;
    
    public PurchaseController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewPurchase([FromBody] Purchase request)
    {
        var purchaseResponse = await _purchaseService.CreateNewPurchase(request);
    
        CommonResponse<PurchaseResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.Created,
            Message = "Successfully create new purchase",
            Data = purchaseResponse
        };
    
        return Created("api/purchases", response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPurchaseById(string id)
    {
        var purchaseResponse = await _purchaseService.GetById(id);

        CommonResponse<PurchaseResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Success Get Purchase",
            Data = purchaseResponse
        };

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPurchases([FromQuery] int page = 1, [FromQuery] int size = 5)
    {
        var purchases = await _purchaseService.GetAll(page, size);

        CommonResponse<PageResponse<PurchaseResponse>> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "successfully get purchase",
            Data = purchases
        };

        return Ok(response);
    }
}