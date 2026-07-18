using CallRatingService.Application.Exceptions;
using MediatR;

namespace CallRatingService.Application.Query
{
    public class GetCustomerRateCardQueryHandler : IRequestHandler<GetCustomerRateCardQuery, CustomerRateCardResponse>
    {
        private readonly IRateCardRepository _cardRepository;
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerRateCardQueryHandler(
            IRateCardRepository cardRepository,
            ICustomerRepository customerRepository)
        {
            _cardRepository = cardRepository;
            _customerRepository = customerRepository;
        }

        public async Task<CustomerRateCardResponse> Handle(GetCustomerRateCardQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerAsync(request.CustomerId);

            if (customer == null) {
                throw new NotFoundException($"Customer with CustomerId {request.CustomerId} does not exist.");
            }

            var result = await _cardRepository.GetRateCardAsync(request.CustomerId);

            return result;

        }
    }
}
