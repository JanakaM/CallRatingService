using CallRatingService.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingService.Application
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerAsync(int customerId);
        Task<List<Customer>> GetCustomersAsync(int page, int numberOfRecords);
    }
}
