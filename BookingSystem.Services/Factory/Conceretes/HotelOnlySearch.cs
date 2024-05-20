using BookingSystem.Common;
using BookingSystem.Common.Enums;
using BookingSystem.Entities;
using BookingSystem.Interfaces.Contracts;
using BookingSystem.Interfaces.Factories;
using BookingSystem.Models.Search;

namespace BookingSystem.Services.Factory.Conceretes
{
    public class HotelOnlySearch : IDestinationSearch
    {
        private readonly ITripxClient _tripxClient;
        private readonly IBaseRepository _repository;

        public HotelOnlySearch(ITripxClient tripxClient, IBaseRepository repository)
        {
            _tripxClient = tripxClient;
            _repository = repository;
        }

        public BookingStatus GetBookingStatusBasedOnSearch(SearchModel searchModel) => BookingStatus.Success;


        public async Task<List<DestinationOptionDTO>> SearchAsync(SearchModel model)
        {
            var hotels = await _tripxClient.GetHotelsAsync(model.Destination);

            List<DestinationOptionDTO> result = new();

            foreach (var hotel in hotels)
            {
                if (hotel.HotelCode == null) continue;

                Destination destinationOptions = new()
                {
                    HotelCode = hotel.HotelCode.Value,
                    Price = RandomGenerator.GenerateInt(400, 4000)
                };

                _repository.AddOrUpdate(destinationOptions);

                result.Add(new()
                {
                    OptionCode = destinationOptions.Code,
                    HotelCode = destinationOptions.HotelCode,
                    Price = destinationOptions.Price
                });
            }

            return result;
        }
    }
}
