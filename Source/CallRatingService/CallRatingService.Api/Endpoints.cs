



using CallRatingService.Application;
using CallRatingService.Application.Command;
using CallRatingService.Model;
using MediatR;
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
            [FromServices] ISender sender)
        {

            var commandRequest = new CallDetailCommandRequest()
            {
                CustomerNumber = request.CustomerNumber,
                CallDate = request.CallDate,
                DestinationNumber = request.DestinationNumber,
                DurationSeconds = request.DurationSeconds
            };

            var id = await sender.Send(commandRequest);

            return id;
        }
    }

}
