using CallRatingService.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingService.Application.Command
{
    public class CallDetailCommandRequest : IRequest<int>
    {
        public int CustomerNumber { get; set; }

        public DateTime CallDate { get; set; }

        public string DestinationNumber { get; set; }

        public int DurationSeconds { get; set; }
    }
}
