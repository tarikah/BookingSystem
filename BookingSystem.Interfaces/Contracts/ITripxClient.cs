using BookingSystem.Models.Search;

namespace BookingSystem.Interfaces.Contracts
{
    public interface ITripxClient
    {
        public Task<List<DestinationHotelDTO>> GetHotelsAsync(string destinationCode);
        public Task<List<DestinationAirportDTO>> GetAirportsAsync(string departureAirport, string arrivalAirport);
    }
}
