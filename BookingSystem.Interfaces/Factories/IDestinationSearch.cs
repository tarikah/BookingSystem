using BookingSystem.Common.Enums;
using BookingSystem.Models.Search;

namespace BookingSystem.Interfaces.Factories
{
    public interface IDestinationSearch
    {
        Task<List<DestinationOptionDTO>> SearchAsync(SearchModel model);
        BookingStatus GetBookingStatusBasedOnSearch(SearchModel searchModel);
    }
}
