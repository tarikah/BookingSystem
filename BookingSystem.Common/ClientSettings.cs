using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Common
{
    public sealed record ClientSettings
    {
        public string JwtTokenKey { get; init; }
        public string TripxAPI { get; set; }
    }
}
