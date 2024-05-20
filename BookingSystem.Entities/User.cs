using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Entities
{
    public record User : BaseEntity
    {
        public string Username { get; init; }
        public byte[] PasswordHash { get; init; }
        public byte[] PasswordSalt { get; init; }
    }
}
