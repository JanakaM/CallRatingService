using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingService.Application.Query
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomersQuery, List<CustomerResponse>>
    {
        private readonly int _numberOfRecordPerPage = 10;
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<CustomerResponse>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetCustomersAsync(request.page, _numberOfRecordPerPage);

            var resposne = customers.Select(c => new CustomerResponse()
            {
                CustomerId = c.CustomerId
            }).ToList();

            return resposne;
        }
    }
}
