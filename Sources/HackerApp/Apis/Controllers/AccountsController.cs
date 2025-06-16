using System.Security.Claims;
using HackerApp.Client.Areas.Login.Dtos;
using HackerApp.Client.Areas.NewGame.Components;
using HackerApp.Client.Infrastructure.InformationHandling.Dtos;
using HackerApp.Infrastructure.Settings.Provisioning.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HackerApp.Apis.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController(ISettingsProvider settingsProvider) : ControllerBase
    {
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromForm] LoginRequestDto dto)
        {
            var loginOk = CheckLogin(dto);

            if (loginOk)
            {
                return await SignInAndRedirectAsync();
            }

            return BadRequest();
        }

        [HttpPost("verifylogin")]
        [AllowAnonymous]
        public IActionResult VerifyLogin([FromBody] LoginRequestDto dto)
        {
            var loginOk = CheckLogin(dto);

            if (loginOk)
            {
                return Ok(new InformationEntriesDto());
            }

            var infos = new InformationEntriesDto
            {
                ErrorMessages = new List<string>
                {
                    "Invalides Login"
                }
            };

            return Ok(infos);
        }

        private bool CheckLogin(LoginRequestDto dto)
        {
            var appSettings = settingsProvider.AppSettings;

            var loginOk = appSettings.UserName == dto.UserName &&
                          appSettings.UserPassword == dto.Password;

            return loginOk;
        }

        private async Task<IActionResult> SignInAndRedirectAsync()
        {
            var claimIdentity = new ClaimsIdentity([
                new Claim(ClaimTypes.Name, settingsProvider.AppSettings.UserName)
            ], CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimIdentity);
            var props = new AuthenticationProperties { AllowRefresh = true, ExpiresUtc = DateTimeOffset.Now.AddDays(7), IsPersistent = true };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);

            return LocalRedirect(NewGamePage.Path);
        }
    }
}