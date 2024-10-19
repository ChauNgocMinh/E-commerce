using E_Commerce.Domain.Entities.Users;
using E_Commerce.Domain.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace E_Commerce.Controllers.UserSite
{
    public class UserController : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(Microsoft.AspNetCore.Identity.UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var token = GenerateJwtToken(model.Email);
                    return Ok(new { token });
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return BadRequest(ModelState);
        }

        private string GenerateJwtToken(string email)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKey12345"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "YourIssuer",
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string email, string displayName, string phoneNumber, DateTime dateOfBirth, string password, string confirmPassword, string avata = null)
        {
            if (password != confirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Mật khẩu xác nhận không khớp.");
                return View();
            }

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = email,
                    Email = email,
                    DisplayName = displayName,
                    PhoneNumber = phoneNumber,
                    DateOfBirth = dateOfBirth,
                    Avata = avata ?? "default-avatar.png" 
                };

                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> InfoUser()
        {
            return View();
        }
    }
}
