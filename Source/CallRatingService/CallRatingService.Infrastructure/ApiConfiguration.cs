using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingService.Infrastructure
{
    public class ApiConfiguration
    {
        public string ConnectionString { get; set; }
    }
}
