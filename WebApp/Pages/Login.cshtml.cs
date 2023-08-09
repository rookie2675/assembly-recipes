using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Repositories.Users;

namespace WebApp.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public required string Username { get; set; }

        [BindProperty]
        public required string Password { get; set; }

        private readonly ILogger<LoginModel> _logger;
        private readonly IUserRepository _userRepository;

        public LoginModel(ILogger<LoginModel> logger, IUserRepository authenticationService)
        {
            _logger = logger;
            _userRepository = authenticationService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!AreCredentialsValid())
                return Page();

            var user = _userRepository.FindByUsernameAndPassword(Username, Password);
                
            if (user is not null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };


                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.Add(TimeSpan.FromDays(7)),
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                return RedirectToPage("/Recipes");
            }

            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return Page();
        }

        public async Task<IActionResult> OnGetLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Recipes");
        }

        private bool AreCredentialsValid()
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                ModelState.AddModelError(string.Empty, "Username cannot be null or empty.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                ModelState.AddModelError(string.Empty, "Password cannot be null or empty.");
                return false;
            }

            return true;
        }
    }
}