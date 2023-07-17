using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain;
using Services.Users;
using System.Security.Claims;

namespace WebApp.Pages
{
    public class UserProfileModel : PageModel
    {
        private readonly IUserService _userService;

        [BindProperty]
        public User User { get; private set; }

        public bool EditMode { get; set; }

        public UserProfileModel(IUserService userService)
        {
            _userService = userService;
            EditMode = false;
        }

        public IActionResult OnGet()
        {
            var user = HttpContext.User;
            string userIdAsString = user.FindFirstValue(ClaimTypes.NameIdentifier);
            long userId = long.Parse(userIdAsString);
            User = _userService.GetById(userId);
            return Page();
        }

        public IActionResult OnPostEditProfile()
        {
            EditMode = true;
            return Page();
        }

        public IActionResult OnPostCancelEdit()
        {
            EditMode = false;
            User = _userService.GetById(User.Id.Value);
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _userService.Update(User);
            EditMode = false;
            User = _userService.GetById(User.Id.Value);

            return Page();
        }
    }
}