using CallRatingService.Application;
using CallRatingService.Application.Command;
using CallRatingService.Application.Exceptions;
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

            appGroup.MapGet("/customer", GetCustomers);

        }

        private static async Task<IResult> AddCallDetail(
            [FromBody]List<CallDetailRequest> request,
            [FromServices] ISender sender)
        {

            if (request == null || request.Count == 0 )
            {
                throw new InvalidRequestException("Empty request");
            }

            var command = new CallDetailCommand()
            {
                CallDetails = request.Select(r => new CallData()
                {
                    CustomerNumber = r.CustomerNumber,
                    CallDate = r.CallDate,
                    DestinationNumber = r.DestinationNumber,
                    DurationSeconds = r.DurationSeconds
                }).ToList()
            };

            var response = await sender.Send(command);

            return Results.Ok(response);
        }

    private static async Task<int> AddCustomerRateCard(
        [FromBody] RateCardRequest request,
        [FromServices] ISender sender)
        {
            if (request == null 
                || request.CustomerId == 0 
                || request.Rates == null 
                || !request.Rates.Any())
            {
                throw new InvalidRequestException("Empty request");
            }

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

    private static async Task<List<CustomerResponse>> GetCustomers(
    [FromQuery] int? page,
    [FromServices] ISender sender)
        {
            var query = new GetCustomersQuery()
            {
                page = page ?? 1,
            };

            var response = await sender.Send(query);

            return response;
        }
    }

}
