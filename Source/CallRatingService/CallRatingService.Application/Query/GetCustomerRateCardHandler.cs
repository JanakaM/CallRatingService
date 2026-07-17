using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingService.Application.Query
{
    public class GetCustomerRateCardHandler : IRequestHandler<GetCustomerRateCardQuery, CustomerRateCardResponse>
    {
        private readonly IRateCardRepository _cardRepository;

        public GetCustomerRateCardHandler(IRateCardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<CustomerRateCardResponse> Handle(GetCustomerRateCardQuery request, CancellationToken cancellationToken)
        {
            var result = await _cardRepository.GetRateCardAsync(request.CustomerId);

            return result;

        }
    }
}
