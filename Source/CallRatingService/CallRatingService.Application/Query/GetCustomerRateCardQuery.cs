using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingService.Application.Query
{
    public class GetCustomerRateCardQuery :IRequest<CustomerRateCardResponse>
    {
        public int CustomerId { get; set; }

    }
}
