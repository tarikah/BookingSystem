using BookingSystem.Common;
using BookingSystem.Common.Enums;
using BookingSystem.Entities;
using BookingSystem.Interfaces.Contracts;
using BookingSystem.Interfaces.Factories;
using BookingSystem.Models.Search;

namespace BookingSystem.Services.Factory.Conceretes
{


    public class HotelAndFlightSearch : IDestinationSearch
    {
        private readonly ITripxClient _tripxClient;
        private readonly IBaseRepository _repository;

        public HotelAndFlightSearch(ITripxClient tripxClient, IBaseRepository repository)
        {
            _tripxClient = tripxClient;
            _repository = repository;
        }

        public BookingStatus GetBookingStatusBasedOnSearch(SearchModel searchModel) => BookingStatus.Success;

        public async Task<List<DestinationOptionDTO>> SearchAsync(SearchModel model)
        {
            var hotels = await _tripxClient.GetHotelsAsync(model.Destination);
            var airports = await _tripxClient.GetAirportsAsync(model.DepartureAirport, model.Destination);

            List<DestinationOptionDTO> result = new();

            foreach (var hotel in hotels)
            {
                if (hotel.HotelCode == null)
                    continue;

                foreach (var airport in airports)
                {
                    Destination destinationOptions = new()
                    {
                        HotelCode = hotel.HotelCode.Value,
                        DepartureAirport = airport.DepartureAirport,
                        ArrivalAirport = airport.ArrivalAirport,
                        FlightCode = airport.FlightCode,
                        Price = RandomGenerator.GenerateInt(400, 4000)
                    };

                    _repository.AddOrUpdate(destinationOptions);

                    //Could have used automapper. But did not include in project. 
                    result.Add(new()
                    {
                        OptionCode = destinationOptions.Code,
                        HotelCode = destinationOptions.HotelCode,
                        Price = destinationOptions.Price,
                        ArrivalAirport = destinationOptions.ArrivalAirport,
                        FlightCode = destinationOptions.FlightCode,
                    });
                }
            }

            return result;
        }
    }
}
