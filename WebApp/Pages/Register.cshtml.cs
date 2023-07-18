using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Users;

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

            try
            {
                var user = new User() { Username = Username, Password = Password, Email = Email };
                user = _userService.Add(user);

                if (user is null) 
                {
                    ViewData["Message"] = "Registration failed. Please try again.";
                    return Page();
                }

                ViewData["Message"] = "Registration successful. You can now log in.";
                ViewData["LoginLink"] = Url.Page("Login");
            }
            catch (ArgumentException)
            {
                ViewData["Message"] = "Registration failed. Username already exists. Please choose a different username.";
            }
            catch (Exception)
            {
                ViewData["Message"] = "Registration failed. An unexpected error occurred. Please try again later.";
            }

            return Page();
        }       
    }
}
