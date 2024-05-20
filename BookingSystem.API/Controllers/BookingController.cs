using BookingSystem.API.Requests.Booking;
using BookingSystem.API.Responses.Booking;
using BookingSystem.Interfaces.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{

    public class BookingController : BaseApiController
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("Book")]
        public async Task<BookResponse> Book(BookRequest request)
        {
            var bookResponse = await _bookingService.BookAsync(request.OptionCode, request.SearchModel);

            return new BookResponse { BookingCode = bookResponse.BookingCode, BookingStatus = bookResponse.BookingStatus };
        }

        //I think that there is no need for separate CheckStatus controller,
        //we can assume that this is BookControllers responsibility
        [HttpPost("CheckStatus")]
        public async Task<CheckStatusResponse> CheckStatus(CheckStatusRequest request)
        {
            var status = await _bookingService.CheckStatusAsync(request.BookingCode);

            return new CheckStatusResponse { Status = status };
        }
    }
}
