﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Models.User
{
    public record TokenOutput
    {
        public string Token { get; init; }
    }
}
