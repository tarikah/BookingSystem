using BookingSystem.Models.Search;

namespace BookingSystem.Interfaces.Contracts
{
    public interface ISearchService
    {
        public Task<List<DestinationOptionDTO>> SearchAsync(SearchModel model);
    }
}
