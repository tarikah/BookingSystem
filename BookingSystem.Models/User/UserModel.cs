using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Models.User
{
    public record UserModel
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
