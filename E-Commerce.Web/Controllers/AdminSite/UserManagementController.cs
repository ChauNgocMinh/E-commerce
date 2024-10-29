using AutoMapper;
using E_Commerce.Application.Response.UserResponse;
using E_Commerce.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers.AdminSite
{
    [Route("Admin/[controller]/[action]")]
    public class UserManagementController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserManagementController(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> UserManagement()
        {
            var users = await _userManager.Users.ToListAsync();
            var userResponses = new List<UserResponse>();

            foreach (var user in users)
            {
                var userResponse = _mapper.Map<UserResponse>(user);
                var roles = await _userManager.GetRolesAsync(user); 
                userResponse.Roles = roles.ToList(); 
                userResponses.Add(userResponse);
            }

            return View(userResponses);
        }
    }
}
