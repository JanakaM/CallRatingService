using CallRatingService.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingServiceModelTests
{
    public class CallTypeTests
    {
        [Fact]
        public void Given_PhoneNumber_GetCallType_UK() {

            // Arrange

            var phoneNumber = "+4407960507456";
            
            // Call

            var type = CallType.GetCallTypeByNumber(phoneNumber);

            // Assert

            Assert.Equal(CallType.UK, type);
            Assert.Equal(CallType.UK.Type, type.Type);
        }

        [Theory]
        [InlineData("+1407960507123", "USA")]
        [InlineData("+3537960507123", "Ireland")]
        [InlineData("+4477960504587", "UKMobile")]
        [InlineData("+9477960504963", "International")]
        [InlineData("9477960504963", "International")] // international higher prefix with out of order format
        [InlineData("33142277555", "International")] // international lower prefix with out of order format
        [InlineData("+33142277555", "International")] // international higher prefix
        public void Given_PhoneNumber_GetCallType(string phoneNumber, string expected)
        {
            // Act

            var type = CallType.GetCallTypeByNumber(phoneNumber);

            // Assert

            Assert.Equal(expected, type.Type);
        }

    }
}
