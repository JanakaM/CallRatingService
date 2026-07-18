using Microsoft.Extensions.Logging;

namespace CallRatingService.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
