using BookingSystem.Common.Enums;
using BookingSystem.Models.Booking;
using BookingSystem.Models.Search;

namespace BookingSystem.Interfaces.Contracts
{
    public interface IBookingService
    {
        Task<BookResponseDTO> BookAsync(string optionCode, SearchModel searchModel);
        Task<BookingStatus> CheckStatusAsync(string bookingCode);
    }
}
