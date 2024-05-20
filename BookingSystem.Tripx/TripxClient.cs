using BookingSystem.Interfaces.Contracts;
using BookingSystem.Models.Search;
using System.Net.Http.Json;

namespace BookingSystem.Tripx
{
    public class TripxClient : ITripxClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public TripxClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<DestinationAirportDTO>> GetAirportsAsync(string departureAirport, string arrivalAirport)
        {
            var httpClient = _httpClientFactory.CreateClient("tripx");
            return
                await httpClient.GetFromJsonAsync<List<DestinationAirportDTO>>($"SearchFlights?departureAirport={departureAirport}&arrivalAirport={arrivalAirport}");
        }

        public async Task<List<DestinationHotelDTO>> GetHotelsAsync(string destinationCode)
        {
            var httpClient = _httpClientFactory.CreateClient("tripx");
            return
                await httpClient.GetFromJsonAsync<List<DestinationHotelDTO>>($"SearchHotels?destinationCode={destinationCode}");
        }
    }
}