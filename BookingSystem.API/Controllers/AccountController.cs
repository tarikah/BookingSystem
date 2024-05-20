using BookingSystem.API.Requests.Account;
using BookingSystem.API.Responses.Account;
using BookingSystem.Interfaces.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ReqisterResponse> Register(RegisterRequest request)
        {
            var tokenOutput = _accountService.Register(request);

            return new ReqisterResponse() { Token = tokenOutput.Token };
        }
    }
}
