using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyAPI.Controllers.Auth.Model;
using MyAPI.Extensions;

namespace MyAPI.Controllers.Auth
{
    [Route("api/V1/")]
    //[ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApiUserClaimsSettings _appSettings;
        private readonly ILogger _logger;

        public UsersController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<ApiUserClaimsSettings> appSettings,
                              IApiUser user, ILogger<UsersController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        [HttpPost("auth")]
        public async Task<ActionResult> SignIn(UserAuthModel userModel)
        {
            var user = new IdentityUser
            {
                UserName = userModel.Email,
                Email = userModel.Email,
                EmailConfirmed = true
            };
            await _signInManager.SignInAsync(user, false);
            await VerifyTokenOauth(user.Email);
            return Ok();
        }

        private async Task<LoginResponseViewModel> VerifyTokenOauth(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);


            var response = new LoginResponseViewModel
            {
                AccessToken = "",
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiresinHrs).TotalSeconds,
                UserToken = new UserTokenViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }
    }
}