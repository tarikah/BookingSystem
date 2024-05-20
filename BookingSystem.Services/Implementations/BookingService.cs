using BookingSystem.Common.Enums;
using BookingSystem.Entities;
using BookingSystem.Interfaces.Contracts;
using BookingSystem.Interfaces.Factories;
using BookingSystem.Models.Booking;
using BookingSystem.Models.Search;

namespace BookingSystem.Services.Implementations
{
    public sealed class BookingService : IBookingService
    {
        private readonly IDestinationSearchFactory _destinationSearchHelperFactory;
        private readonly IBaseRepository _repository;

        public BookingService(IDestinationSearchFactory destinationSearchHelperFactory, IBaseRepository repository)
        {
            _destinationSearchHelperFactory = destinationSearchHelperFactory;
            _repository = repository;
        }
        private void BackgroundSync(SearchModel searchModel, Booking? booking)
        {
            //This can be done with Hangfire
            _ = Task.Run(async () =>
            {
                await Task.Delay(booking.SleepTime.Value * 1000);

                var destinationSearchHelper = _destinationSearchHelperFactory.GetDestinationSearch(searchModel);

                booking.Status = destinationSearchHelper.GetBookingStatusBasedOnSearch(searchModel);

                _repository.Replace(booking);
            });
        }
        public async Task<BookResponseDTO> BookAsync(string optionCode, SearchModel searchModel)
        {
            var booking = new Booking { Type = Common.Enums.BookingType.HotelOnly };

            _repository.Insert(booking);

            BackgroundSync(searchModel, booking);

            return new BookResponseDTO
            {
                BookingCode = booking.Code,
                BookingStatus = booking.Status,
            };
        }

        public async Task<BookingStatus> CheckStatusAsync(string bookingCode)
        {
            var booking = _repository.FirstOrDefault<Booking>(x => x.Code == bookingCode);

            return booking.Status;
        }
    }
}
