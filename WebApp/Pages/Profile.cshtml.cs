using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain;
using Services.Users;
using System.Security.Claims;

namespace WebApp.Pages
{
    public class UserProfileModel : PageModel
    {
        private readonly ILogger<UserProfileModel> _logger;
        private readonly IUserService _userService;

        [BindProperty]
        public User? UserProfile { get; set; }

        public bool EditMode { get; set; }

        public UserProfileModel(ILogger<UserProfileModel> logger, IUserService userService)
        {
            EditMode = false;
            _logger = logger;
            _userService = userService;
        }

        public IActionResult OnGet()
        {
            var user = HttpContext.User;
            string? userIdAsString = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdAsString))
                return Unauthorized();

            if (!long.TryParse(userIdAsString, out long userId))
                return NotFound();

            UserProfile = _userService.GetById(userId);

            if (User is null)
                return NotFound();

            return Page();
        }

        public IActionResult OnGetEditProfile()
        {   
            var user = HttpContext.User;
            string? userIdAsString = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdAsString))
                return Unauthorized();

            if (!long.TryParse(userIdAsString, out long userId))
                return NotFound();

            UserProfile = _userService.GetById(userId);

            if (UserProfile is null)
                return NotFound();

            EditMode = true;

            return Page();
        }

        public IActionResult OnPostEditProfile()
        {
            EditMode = true;
            return Page();
        }

        public IActionResult OnPostDeleteProfile()
        {

            var user = HttpContext.User;
            string? userIdAsString = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdAsString))
                return Unauthorized();

            if (!long.TryParse(userIdAsString, out long userId))
                return NotFound();

            _logger.LogInformation("Request received to delete user with ID: " + userId);
            _userService.Delete(userId);

            return Page();
        }

        public IActionResult OnPostCancelEdit()
        {
            EditMode = false;
            UserProfile = _userService.GetById(UserProfile.Id.Value);
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _userService.Update(UserProfile);
            EditMode = false;
            UserProfile = _userService.GetById(UserProfile.Id.Value);

            return Page();
        }
    }
}