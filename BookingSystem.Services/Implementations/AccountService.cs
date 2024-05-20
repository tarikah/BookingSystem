using BookingSystem.Common;
using BookingSystem.Entities;
using BookingSystem.Interfaces.Contracts;
using BookingSystem.Models.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;

namespace BookingSystem.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly string _tokenKey;
        private readonly IBaseRepository _repository;
        public AccountService(IOptions<ClientSettings> clientSettings, IBaseRepository repository)
        {
            _tokenKey = clientSettings.Value.JwtTokenKey;
            _repository = repository;
        }

        public TokenOutput Register(UserModel userModel)
        {
            CreatePasswordHash(userModel.Password, out byte[] passwordHash, out byte[] passwordSalt);

            _repository.Insert(new User
            {
                Username = userModel.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            });

            var token = CreateToken(userModel);

            return new TokenOutput
            {
                Token = token
            };
        }

        public TokenOutput Login(UserModel model)
        {
            var user = _repository.FirstOrDefault<User>(x => x.Username == model.Username);

            if (user == null)
                throw new AuthenticationException("User with that username not found");

            if (!VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                throw new AuthenticationException("Incorrect password");

            var token = CreateToken(model);

            return new TokenOutput
            {
                Token = token
            };
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        private string CreateToken(UserModel user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_tokenKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
