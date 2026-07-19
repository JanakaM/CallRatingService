using CallRatingService.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingService.Application.Command
{
    public class CallDetailCommand : IRequest<List<RatedOutputResponse>>
    {
        public List<CallData> CallDetails { get; set; }
    }

    public class CallData
    {
        public int CustomerNumber { get; set; }

        public string CallDate { get; set; }

        public string DestinationNumber { get; set; }

        public int DurationSeconds { get; set; }
    }
}
