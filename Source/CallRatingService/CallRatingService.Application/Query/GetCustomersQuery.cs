using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingService.Application.Query
{
    public class GetCustomersQuery : IRequest<List<CustomerResponse>>
    {
        public int page { get; set; }
    }
}
