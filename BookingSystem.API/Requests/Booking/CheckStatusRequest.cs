using System.ComponentModel.DataAnnotations;

namespace BookingSystem.API.Requests.Booking
{
    public sealed record CheckStatusRequest
    {
        [Required]
        public string BookingCode { get; init; }
    }
}
