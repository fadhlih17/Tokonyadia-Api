using System.Net;
using Microsoft.AspNetCore.Mvc;
using Tokonyadia_Api.DTO;
using Tokonyadia_Api.Entities;
using Tokonyadia_Api.Services;

namespace Tokonyadia_Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewProduct([FromBody] Product request)
    {
        var productResponse = await _productService.CreateNewProduct(request);

        CommonResponse<ProductResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.Created,
            Message = "Successfully create new customer",
            Data = productResponse
        };

        return Created("api/products", response);
    }
    
    // Get with join by id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(string id)
    {
        var productResponse = await _productService.GetById(id);
        
        CommonResponse<ProductResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Successfully get products",
            Data = productResponse
        };
        
        return Ok(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllProduct([FromQuery] string? name, [FromQuery] int page = 1, [FromQuery] int size = 5)
    {
        var products = await _productService.GetAll(name, page, size);
       
        CommonResponse<PageResponse<ProductResponse>> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Successfully get products",
            Data = products
        };
        
        return Ok(response);
    }

    [HttpDelete("{id}")] // path variable
    public async Task<IActionResult> DeleteProductById(string id)
    {
        await _productService.DeleteById(id);

        CommonResponse<string> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Successfully Delete Customer"
        };

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody]Product request)
    {
        var product = await _productService.Update(request);

        CommonResponse<ProductResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Successfully Update Product",
            Data = product
        };

        return Ok(response);
    }

}