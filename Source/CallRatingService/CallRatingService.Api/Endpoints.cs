



using CallRatingService.Application;
using CallRatingService.Model;
using Microsoft.AspNetCore.Mvc;

namespace CallRatingService.Api
{
    public static class Endpoints
    {

        public static void MapEndpoints(this WebApplication app)
        {
            var appGroup = app.MapGroup("/callrating");

            appGroup.MapPost("/callDetail", AddCallDetail);
        }

        private static async Task<int> AddCallDetail([FromBody]CallDetailRequest request,
            [FromServices] ICallDetailRepository detailRepository)
        {
            var detail = new CallDetail()
            {
                CustomerNumber = request.CustomerNumber,
                CallDate = request.CallDate,
                DestinationNumber = request.DestinationNumber,
                DurationSeconds = request.DurationSeconds
            };

            var id = detailRepository.SaveCallDetail(detail);

            return id;
        }
    }

}
