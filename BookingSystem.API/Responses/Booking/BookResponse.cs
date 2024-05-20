using BookingSystem.Common.Enums;

namespace BookingSystem.API.Responses.Booking
{
    public sealed record BookResponse
    {
        public string BookingCode { get; init; }
        public BookingStatus BookingStatus { get; init; }
    }
}
