using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.Models;
using Project.Services;

namespace Project.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly JwtService _jwtService;

        public LoginModel(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [BindProperty]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = Models.User.Authenticate(Username, Password);
            if (user == null)
            {
                ErrorMessage = "Invalid username or password";
                return Page();
            }

            var token = _jwtService.SignToken(user);

            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            });

            return RedirectToPage("/Index");
        }
    }
}
