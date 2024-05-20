using BookingSystem.Common.Enums;

namespace BookingSystem.API.Responses.Booking
{
    public sealed record CheckStatusResponse
    {
        public BookingStatus Status { get; init; }
    }
}
