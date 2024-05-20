using BookingSystem.Models.Search;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.API.Requests.Booking
{
    public record BookRequest
    {
        [Required]
        public string OptionCode { get; set; }

        [Required]
        public SearchModel SearchModel { get; set; }
    }
}
