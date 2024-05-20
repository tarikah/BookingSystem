using BookingSystem.Models.User;

namespace BookingSystem.Interfaces.Contracts
{
    public interface IAccountService
    {
        TokenOutput Register(UserModel userModel);
        TokenOutput Login(UserModel userModel);
    }
}
