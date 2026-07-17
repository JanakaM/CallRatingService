using CallRatingService.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingService.Application.Command
{
    public class CallDetailCommandHandler : IRequestHandler<CallDetailCommand, int>
    {
        private readonly ICallDetailRepository _detailRepository;

        public CallDetailCommandHandler(ICallDetailRepository detailRepository)
        {
            _detailRepository = detailRepository;
        }

        public async Task<int> Handle(CallDetailCommand request, CancellationToken cancellationToken)
        {

            var callDetail = new CallDetail()
            {
                CustomerNumber = request.CustomerNumber,
                CallDate = request.CallDate,
                DestinationNumber = request.DestinationNumber,
                DurationSeconds = request.DurationSeconds
            };

            var id = await _detailRepository.SaveCallDetail(callDetail);

            return id;
        }
    }
}
