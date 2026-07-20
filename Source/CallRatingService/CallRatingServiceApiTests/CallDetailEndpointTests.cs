using CallRatingService.Application;
using CallRatingService.Application.Command;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using System.Net;
using System.Net.Http.Json;

namespace CallRatingServiceApiTests
{
    public class CallDetailEndpointTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<ISender> _mediatorMock;

        public CallDetailEndpointTests(WebApplicationFactory<Program> factory
            )
        {
            _mediatorMock = new Mock<ISender>();

            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(ISender));
                    if (descriptor != null) services.Remove(descriptor);

                    // Add mock
                    services.AddSingleton(_mediatorMock.Object);
                });
            });
        }

        [Fact]
        public async Task GivenCallDetailRequest_sendCommand_ReceiveCallDetailResponse()
        {
            // Arrange
            var client = _factory.CreateClient();

            var validPayload = new List<object>
            {
                new { CustomerNumber = 1001, CallDate = "2026-07-20T12:00:00.000Z", DestinationNumber = "+447960507654", DurationSeconds = 120 }
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<CallDetailCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<RatedOutputResponse> // Adapt target return class name to your exact mapping layer
                {
                    new RatedOutputResponse { CallType = "UK" }
                });

            // Act
            var response = await client.PostAsJsonAsync("/api/callDetail", validPayload);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            _mediatorMock.Verify(x => x.Send(It.Is<CallDetailCommand>(cmd =>
                cmd.CallDetails.Count == 1 &&
                cmd.CallDetails[0].CustomerNumber == 1001 &&
                cmd.CallDetails[0].DestinationNumber == "+447960507654"
            ), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task AddCallDetail_EmptyPayload_ReturnsBadRequest()
        {
            // Arrange
            var client = _factory.CreateClient();
            var emptyPayload = new List<object>(); 

            // Act
            var response = await client.PostAsJsonAsync("/api/callDetail", emptyPayload);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

             _mediatorMock.Verify(x => x.Send(It.IsAny<CallDetailCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }

}
