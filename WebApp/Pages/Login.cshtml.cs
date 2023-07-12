using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Contracts;

namespace WebApp.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public required string Username { get; set; }

        [BindProperty]
        public required string Password { get; set; }

        private readonly ILogger<LoginModel> _logger;
        private readonly IAuthenticationService _authenticationService;

        public LoginModel(ILogger<LoginModel> logger, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public IActionResult OnPost()
        {
            if (!AreCredentialsValid())
                return Page();

            var account = _authenticationService.SignIn(Username, Password);

            if (account is null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return Page();
            }

            if (account.Id.HasValue)
                StoreAuthenticationCookie(account.Id.Value);

            LogConfirmationMessage();

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

        private void StoreAuthenticationCookie(long userId)
        {
            var options = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(7),
                HttpOnly = true,
                Secure = Request.IsHttps,
                SameSite = SameSiteMode.Strict
            };

            Response.Cookies.Append("UserId", userId.ToString(), options);
        }

        private void LogConfirmationMessage() => _logger.LogInformation("User {Username} logged in successfully.", Username);
    }
}