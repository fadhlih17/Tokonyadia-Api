using Microsoft.AspNetCore.Mvc;
using Tokonyadia_Api.DTO;
using Tokonyadia_Api.Entities;

namespace Tokonyadia_Api.Services;

public interface ICustomerService
{
    // Task<CustomerResponse> CreateNewCustomer(Customer payload);
    // Task<CustomerResponse> GetById(string id);
    // Task<PageResponse<CustomerResponse>> GetAll(string? name, int page, int size);
    // Task<CustomerResponse> Update(Customer payload);
    // Task DeleteById(string id);

    Task<Customer> Create(Customer customer);
    Task<Customer> Update(Customer customer);
}