using BookingSystem.Interfaces.Contracts;
using BookingSystem.Interfaces.Factories;
using BookingSystem.Models.Search;

namespace BookingSystem.Services.Implementations
{
    public sealed class SearchService : ISearchService
    {
        private readonly IDestinationSearchFactory _destinationSearchHelperFactory;

        public SearchService(IDestinationSearchFactory destinationSearchHelperFactory)
        {
            _destinationSearchHelperFactory = destinationSearchHelperFactory;
        }

        public async Task<List<DestinationOptionDTO>> SearchAsync(SearchModel model)
        {
            var destinationSearch = _destinationSearchHelperFactory.GetDestinationSearch(model);

            var destinations = await destinationSearch.SearchAsync(model);

            return new(destinations);
        }
    }
}
