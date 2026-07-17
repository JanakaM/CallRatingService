using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingService.Application.Command
{
    public class RateCardCommand : IRequest<int>
    {
        public int CustomerId { get; set; }

        public List<RatesCommand> Rates { get; set; }
    }

    public class RatesCommand

    {
        public string CallType { get; set; }

        public decimal CostPerMinute { get; set; }
    } 
}
