using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingService.Application.Exceptions
{
    public class InvalidRequestException : Exception
    {
        public InvalidRequestException(string message) : base(message) { }
    }
}
