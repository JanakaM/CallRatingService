using CallRatingService.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingService.Application.Command
{
    public class RateCardCommandHandler : IRequestHandler<RateCardCommand, int>
    {
        private readonly IRateCardRepository _cardRepository;

        public RateCardCommandHandler(IRateCardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public Task<int> Handle(RateCardCommand request, CancellationToken cancellationToken)
        {
            var rateCard = new CustomerRateCard()
            {
                CustomerId = request.CustomerId,
                Rates = request.Rates.Select(rates => new CustomerRate()
                {
                    CustomerId = request.CustomerId,
                    CallType = rates.CallType,
                    CostPerMinute = rates.CostPerMinute
                }).ToList()
            };

            var id = _cardRepository.UpsertRateCardAsync(rateCard);

            return id;
        }
    }
}
