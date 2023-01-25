using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tokonyadia_Api.DTO;
using Tokonyadia_Api.Entities;
using Tokonyadia_Api.Repositories;
using Tokonyadia_Api.Services;

namespace Tokonyadia_Api.Controllers;

// [ApiController]
[Route("api/stores")]
public class StoreController : BaseController
{
    private readonly IStoreService _storeService;

    public StoreController(IStoreService storeService)
    {
        _storeService = storeService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewStore([FromBody] Store request)
    {
        var store = await _storeService.CreateNewStore(request);

        CommonResponse<StoreResponse> response = new CommonResponse<StoreResponse>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Successfully create store",
            Data = store
        };

        return Created("api/stores",response);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllStores([FromQuery] string? name, [FromQuery] int page = 1, [FromQuery] int size = 5)
    {
        var stores = await _storeService.GetAll(name, page, size);

        CommonResponse<PageResponse<StoreResponse>> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Success get stores",
            Data = stores
        };

        return Ok(response);
    }
    
    [HttpGet("{id}")] // path variable : to take data specifically
    public async Task<IActionResult> GetStoreById(string id)
    {
        var store = await _storeService.GetById(id);

        CommonResponse<StoreResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Success get store",
            Data = store
        };

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStore([FromBody] Store request)
    {
        var store = await _storeService.Update(request);

        CommonResponse<StoreResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Success Update Store",
            Data = store
        };

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStoreById(string id)
    {
        await _storeService.DeleteById(id);
        CommonResponse<StoreResponse> response = new CommonResponse<StoreResponse>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Success delete store"
        };

        return Ok(response);
    }
}