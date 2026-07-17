



using CallRatingService.Application;
using CallRatingService.Application.Command;
using CallRatingService.Application.Query;
using CallRatingService.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CallRatingService.Api
{
    public static class Endpoints
    {

        public static void MapEndpoints(this WebApplication app)
        {
            var appGroup = app.MapGroup("/api");

            appGroup.MapPost("/callDetail", AddCallDetail);

            appGroup.MapPost("/rateCard", AddCustomerRateCard);

            appGroup.MapGet("/rateCard", GetCustomerRateCard);
        }

        private static async Task<int> AddCallDetail(
            [FromBody]CallDetailRequest request,
            [FromServices] ISender sender)
        {

            var command = new CallDetailCommand()
            {
                CustomerNumber = request.CustomerNumber,
                CallDate = request.CallDate,
                DestinationNumber = request.DestinationNumber,
                DurationSeconds = request.DurationSeconds
            };

            var id = await sender.Send(command);

            return id;
        }

    private static async Task<int> AddCustomerRateCard(
        [FromBody] RateCardRequest request,
        [FromServices] ISender sender)
        {

            var command = new RateCardCommand()
            {
                CustomerId = request.CustomerId,
                Rates = request.Rates.Select(rates => new RatesCommand
                {
                    CallType = rates.CallType,
                    CostPerMinute = rates.CostPerMinute,
                }).ToList()
            };
            var id = await sender.Send(command);

            return id;
    }

    private static async Task<CustomerRateCardResponse> GetCustomerRateCard(
    [FromQuery] int customerId,
    [FromServices] ISender sender)
        {
            var query = new GetCustomerRateCardQuery()
            {
                CustomerId = customerId
            };

            var response = await sender.Send(query);

            return response;
        }
    }

}
