using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Models.Search
{
    public record DestinationHotelDTO
    {
        public int Id { get; init; }
        public int? HotelCode { get; init; }
        public string HotelName { get; init; }
        public string DestinationCode { get; init; }
        public string City { get; init; }
    }
}
