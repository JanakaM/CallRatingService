using CallRatingService.Application;
using CallRatingService.Model;
using Microsoft.EntityFrameworkCore;

namespace CallRatingService.Infrastructure
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ICallRatingServiceDbContext _dbContext;

        public CustomerRepository(ICallRatingServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> GetCustomerAsync(int customerId)
        {
            var customer = await _dbContext.Customers
                .FirstOrDefaultAsync(x => x.CustomerId == customerId);   

            return customer;
        }

        public async Task<List<Customer>> GetCustomersAsync(int page, int numberOfRecords)
        {
            int recordsToSkip = (page - 1) * numberOfRecords;

            var customers = await _dbContext.Customers
                .OrderBy(x => x.CustomerId)
                .Skip(recordsToSkip)
                .Take(numberOfRecords)
                .ToListAsync();

            return customers;
        }

        public async Task<Customer?> GetCustomerWithRatesAsync(int customerId)
        {
            var customer =  await _dbContext.Customers
                            .Include(c => c.CustomerRateCard)
                            .ThenInclude(card => card.Rates)
                            .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            return customer;
        }
    }
}
