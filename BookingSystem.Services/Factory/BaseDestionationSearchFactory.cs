using BookingSystem.Interfaces.Contracts;
using BookingSystem.Interfaces.Factories;
using BookingSystem.Models.Search;
using BookingSystem.Services.Factory.Conceretes;

namespace BookingSystem.Services.Factory
{
    public sealed class BaseDestionationSearchFactory : IDestinationSearchFactory
    {
        private readonly ITripxClient _tripxClient;
        private readonly IBaseRepository _repository;

        public BaseDestionationSearchFactory(ITripxClient tripxClient, IBaseRepository repository)
        {
            _tripxClient = tripxClient;
            _repository = repository;
        }

        public IDestinationSearch GetDestinationSearch(SearchModel searchModel) => searchModel switch
        {
            var s when s.FromDate <= DateTime.Now.AddDays(45) => new LastMinuteSearch(_tripxClient, _repository),
            var s when string.IsNullOrEmpty(s.DepartureAirport) => new HotelOnlySearch(_tripxClient, _repository),
            _ => new HotelOnlySearch(_tripxClient, _repository)
        };

    }


}
