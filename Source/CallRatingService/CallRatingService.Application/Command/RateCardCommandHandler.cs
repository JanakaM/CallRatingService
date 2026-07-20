using CallRatingService.Application.Exceptions;
using CallRatingService.Model;
using CallRatingService.Model.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingService.Application.Command
{
    public class RateCardCommandHandler : IRequestHandler<RateCardCommand, int>
    {
        private readonly IRateCardRepository _cardRepository;
        private readonly ICustomerRepository _customerRepository;

        public RateCardCommandHandler(
            IRateCardRepository cardRepository,
            ICustomerRepository customerRepository)
        {
            _cardRepository = cardRepository;
            _customerRepository = customerRepository;
        }

        public async Task<int> Handle(RateCardCommand request, CancellationToken cancellationToken)
        {
            await Validate(request);

            var rateCard = new CustomerRateCard()
            {
                CustomerId = request.CustomerId,
                Rates = request.Rates.Select(rates => new CustomerRate()
                {
                    CustomerRateCustomerId = request.CustomerId,
                    CallType = rates.CallType,
                    CostPerMinute = rates.CostPerMinute
                }).ToList()
            };

            var id = await _cardRepository.UpsertRateCardAsync(rateCard);

            return id;
        }

        private async Task Validate(RateCardCommand request)
        {
            var customer = await _customerRepository.GetCustomerAsync(request.CustomerId);

            if (customer == null)
            {
                throw new NotFoundException($"Customer with CustomerId {request.CustomerId} does not exist.");
            }

            foreach (var rateCard in request.Rates) {

                var callType = CallType.List().Find(x => x.Type == rateCard.CallType);
                
                if (callType == null)
                {
                    throw new NotFoundException($"Call Type does not exist : {rateCard.CallType}");
                }
            }

        }
    }
}
