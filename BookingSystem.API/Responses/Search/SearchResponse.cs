using BookingSystem.Models.Search;

namespace BookingSystem.API.Responses.Search
{
    public sealed record SearchResponse
    {
        public List<DestinationOptionDTO> Options { get; set; } = new();
    }
}
