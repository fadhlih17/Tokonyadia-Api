using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tokonyadia_Api.DTO;
using Tokonyadia_Api.Entities;
using Tokonyadia_Api.Middlewares;
using Tokonyadia_Api.Repositories;
using Tokonyadia_Api.Services;

namespace Tokonyadia_Api.Controllers;

// [ApiController]
[Route("api/customers")]
public class CustomerController : BaseController
{
    //private readonly ICustomerService _customerService;

    private readonly AppDbContext _appDbContext;
    private readonly ILogger _logger;

    public CustomerController(AppDbContext appDbContext, ILogger<CustomerController> logger)
    {
        _appDbContext = appDbContext;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAllCustomer()
    {
        var customers = await _appDbContext.Customers.ToListAsync();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(string id)
    {
        try
        {
            var customer = await _appDbContext.Customers.FirstOrDefaultAsync(customer => customer.Id.Equals(Guid.Parse(id)));
            if (customer is null) throw new Exception("Customer not found");
            return Ok(customer);
        }
        catch (Exception e)
        {
            return new StatusCodeResult(500);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
    {
        try
        {
            if (customer.Id == Guid.Empty) return new NotFoundObjectResult("Customer not found");
            var currentCustomer = await _appDbContext.Customers.FirstOrDefaultAsync(c => c.Id.Equals(customer.Id));
            if (currentCustomer is null) return new NotFoundObjectResult("customer not found");
            
            var entry = _appDbContext.Customers.Attach(customer);
            _appDbContext.Customers.Update(customer);
            
            await _appDbContext.SaveChangesAsync();
            return Ok(entry.Entity);

        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new StatusCodeResult(500);

        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomerById(string id)
    {
        try
        {
            var customer = await _appDbContext.Customers.FirstOrDefaultAsync(customer => customer.Id.Equals(Guid.Parse(id)));
            if (customer is null) return NotFound("customer not found");
            _appDbContext.Customers.Remove(customer);
            await _appDbContext.SaveChangesAsync();
            return Ok("customer successfully deleted");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new StatusCodeResult(500);
        }
    }


    // Create Customer
    // [HttpPost]
    // public async Task<IActionResult> CreateNewCustomer([FromBody] Customer request)
    // {
    //     var customerResponse = await _customerService.CreateNewCustomer(request);
    //
    //     CommonResponse<CustomerResponse> response = new()
    //     {
    //         StatusCode = (int)HttpStatusCode.Created,
    //         Message = "Successfully create new customer",
    //         Data = customerResponse
    //     };
    //
    //     return Created("api/customers", response);
    // }
    //
    // // Get all Customers
    // [HttpGet]
    // public async Task<IActionResult> GetAllCustomers([FromQuery] string? name, [FromQuery] int page = 1, [FromQuery] int size = 5)
    // {
    //    var customers = await _customerService.GetAll(name, page, size);
    //
    //    CommonResponse<PageResponse<CustomerResponse>> response = new()
    //    {
    //        StatusCode = (int)HttpStatusCode.OK,
    //        Message = "Successfully get data",
    //        Data = customers
    //    };
    //
    //    return Ok(response);
    // }
    //
    // // Get Customer By Id
    // [HttpGet("{id}")] // path variable : to take data specifically
    // public async Task<IActionResult> GetCustomerById(string id)
    // {
    //     var customer = await _customerService.GetById(id);
    //
    //     CommonResponse<CustomerResponse> response = new()
    //     {
    //         StatusCode = (int)HttpStatusCode.OK,
    //         Message = "Successfully Get Customer",
    //         Data = customer
    //     };
    //
    //     return Ok(response);
    // }
    //
    // // Update Customer
    // [HttpPut]
    // public async Task<IActionResult> UpdateCustomer([FromBody] Customer request)
    // {
    //     var customer = await _customerService.Update(request);
    //
    //     CommonResponse<CustomerResponse> response = new()
    //     {
    //         StatusCode = (int)HttpStatusCode.OK,
    //         Message = "Successfully Update Customer",
    //         Data = customer
    //     };
    //
    //     return Ok(response);
    // }

    // Delete Customer
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteCustomerById(string id)
    // {
    //     await _customerService.DeleteById(id);
    //
    //     CommonResponse<CustomerResponse> response = new()
    //     {
    //         StatusCode = (int)HttpStatusCode.OK,
    //         Message = "Success Delete Customer",
    //     };
    //
    //     return Ok(response);
    // }
}