using BookingSystem.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Interfaces.Contracts
{
    public interface IAccountService
    {
        TokenOutput Register(UserModel userModel);
        TokenOutput Login(UserModel userModel);
    }
}
