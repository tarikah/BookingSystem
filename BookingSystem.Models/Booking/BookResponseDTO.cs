using BookingSystem.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Models.Booking
{
    public record BookResponseDTO
    {
        public string BookingCode { get; init; }
        public BookingStatus BookingStatus { get; init; }
    }
}
