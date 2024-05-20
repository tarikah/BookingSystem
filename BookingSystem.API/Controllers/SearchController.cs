using BookingSystem.API.Requests.Search;
using BookingSystem.API.Responses.Search;
using BookingSystem.Interfaces.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{

    public class SearchController : BaseApiController
    {
        private readonly ISearchService _searchDestinationService;

        public SearchController(ISearchService searchDestinationService)
        {
            _searchDestinationService = searchDestinationService;
        }

        [HttpPost("Search")]
        public async Task<SearchResponse> Search(SearchRequest request)
        {
            var destinations = await _searchDestinationService.SearchAsync(request);

            return new SearchResponse { Options = destinations };
        }
    }
}
