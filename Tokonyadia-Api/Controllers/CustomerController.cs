using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tokonyadia_Api.DTO;
using Tokonyadia_Api.Entities;
using Tokonyadia_Api.Repositories;
using Tokonyadia_Api.Services;

namespace Tokonyadia_Api.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService )
    {
        _customerService = customerService;
    }

    // Create Customer
    [HttpPost]
    public async Task<IActionResult> CreateNewCustomer([FromBody] Customer request)
    {
        var customerResponse = await _customerService.CreateNewCustomer(request);

        CommonResponse<CustomerResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.Created,
            Message = "Successfully create new customer",
            Data = customerResponse
        };

        return Created("api/customers", response);
    }
    
    // Get all Customers
    [HttpGet]
    public async Task<IActionResult> GetAllCustomers([FromQuery] string? name, [FromQuery] int page = 1, [FromQuery] int size = 5)
    {
       var customers = await _customerService.GetAll(name, page, size);

       CommonResponse<PageResponse<CustomerResponse>> response = new()
       {
           StatusCode = (int)HttpStatusCode.OK,
           Message = "Successfully get data",
           Data = customers
       };

       return Ok(response);
    }
    
    // Get Customer By Id
    [HttpGet("{id}")] // path variable : to take data specifically
    public async Task<IActionResult> GetCustomerById(string id)
    {
        var customer = await _customerService.GetById(id);

        CommonResponse<CustomerResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Successfully Get Customer",
            Data = customer
        };

        return Ok(response);
    }

    // Update Customer
    [HttpPut]
    public async Task<IActionResult> UpdateCustomer([FromBody] Customer request)
    {
        var customer = await _customerService.Update(request);

        CommonResponse<CustomerResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Successfully Update Customer",
            Data = customer
        };

        return Ok(response);
    }

    // Delete Customer
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomerById(string id)
    {
        await _customerService.DeleteById(id);

        CommonResponse<CustomerResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Success Delete Customer",
        };

        return Ok(response);
    }
}