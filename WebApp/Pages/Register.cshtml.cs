using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Contracts;

namespace WebApp.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;

        [BindProperty]
        public required string Username { get; set; }

        [BindProperty]
        public required string Email { get; set; }

        [BindProperty]
        public required string Password { get; set; }

        public RegisterModel(IUserService userService) => _userService = userService;

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = new User() { Username = Username, Password = Password, Email = Email };
            user = _userService.Add(user);

            if (user is null)
            {
                ViewData["Message"] = "Registration failed. Please try again.";
            }

            else
            {
                ViewData["Message"] = "Registration successful. You can now log in.";
                ViewData["LoginLink"] = Url.Page("Login");
            }

            return Page();
        }
    }
}
