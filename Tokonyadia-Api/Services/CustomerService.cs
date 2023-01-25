using Microsoft.EntityFrameworkCore;
using Tokonyadia_Api.DTO;
using Tokonyadia_Api.Entities;
using Tokonyadia_Api.Middlewares;
using Tokonyadia_Api.Repositories;

namespace Tokonyadia_Api.Services;

public class CustomerService : ICustomerService
{
    private readonly IRepository<Customer> _customerRepository;
    private readonly Ipersistance _persistance;

    public CustomerService(IRepository<Customer> customerRepository, Ipersistance persistance)
    {
        _customerRepository = customerRepository;
        _persistance = persistance;
    }

    public async Task<Customer> Create(Customer customer)
    {
        var save = await _customerRepository.Save(customer);
        await _persistance.SaveChangesAsync();
        return save;
    }

    public async Task<Customer> Update(Customer customer)
    {
        var update = _customerRepository.Update(customer);
        await _persistance.SaveChangesAsync();
        return update;
    }


      // public async Task<CustomerResponse> CreateNewCustomer(Customer payload)
    // {
    //     var result = await _persistance.ExecuteTransactionAsync(async () =>
    //     {
    //         var customer = await _customerRepository.Save(payload);
    //         await _persistance.SaveChangesAsync();
    //         return customer;
    //     });
    //
    //     CustomerResponse response = new()
    //     {
    //         Id = result.Id.ToString(),
    //         Name = result.CustomerName,
    //         PhoneNumber = result.PhoneNumber,
    //         Address = result.Address,
    //     };
    //     
    //     return response;
    // }
    //
    // public async Task<CustomerResponse> GetById(string id)
    // {
    //     var customer = await _customerRepository.Find(customer => customer.Id.Equals(Guid.Parse(id)));
    //
    //     if (customer is null) throw new Exception("Customer Not Found");
    //
    //     CustomerResponse response = new CustomerResponse
    //     {
    //         Id = customer.Id.ToString(),
    //         Name = customer.CustomerName,
    //         PhoneNumber = customer.PhoneNumber,
    //         Address = customer.Address,
    //     };
    //
    //     return response;
    // }
    //
    // public async Task<PageResponse<CustomerResponse>> GetAll(string? name, int page, int size)
    // {
    //     var customers = await _customerRepository.FindAll(
    //         criteria: p => EF.Functions.Like(p.CustomerName, $"%{name}%"),
    //         page: page,
    //         size: size
    //         );
    //     
    //     var customerResponses = customers.Select(customer =>
    //     {
    //         return new CustomerResponse
    //         {
    //             Id = customer.Id.ToString(),
    //             Name = customer.CustomerName,
    //             PhoneNumber = customer.PhoneNumber,
    //             Address = customer.Address,
    //         };
    //     }).ToList();
    //     
    //     var totalPages = (int)Math.Ceiling(await _customerRepository.Count() / (decimal)size);
    //
    //     PageResponse<CustomerResponse> pageResponse = new()
    //     {
    //         Content = customerResponses,
    //         TotalPage = totalPages,
    //         TotalElement = customerResponses.Count
    //     };
    //
    //     return pageResponse;
    // }
    //
    // public async Task<CustomerResponse> Update(Customer payload)
    // {
    //     if (payload.Id == Guid.Empty) throw new Exception("Customer Not Found");
    //     
    //     _customerRepository.Update(payload);
    //     await _persistance.SaveChangesAsync();
    //
    //     CustomerResponse response = new()
    //     {
    //         Id = payload.Id.ToString(),
    //         Name = payload.CustomerName,
    //         PhoneNumber = payload.PhoneNumber,
    //         Address = payload.Address,
    //     };
    //
    //     return response;
    // }

    // public async Task DeleteById(string id)
    // {
    //     var customer = await _customerRepository.FindById(Guid.Parse(id));
    //     if (customer is null) throw new Exception("Customer Not Found");
    //     _customerRepository.Delete(customer);
    //     await _persistance.SaveChangesAsync();
    // }
    
    // public async Task<Customer> Create(Customer customer)
    // {
    //     var save = await _customerRepository.Save(customer);
    //     await _persistance.SaveChangesAsync();
    //     return save;
    // }
    //
    // public async Task<Customer> Update(Customer customer)
    // {
    //     var update = _customerRepository.Update(customer);
    //     await _persistance.SaveChangesAsync();
    //     return update;
    // }
}