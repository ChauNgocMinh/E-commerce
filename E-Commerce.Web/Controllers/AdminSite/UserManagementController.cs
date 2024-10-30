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

        // Action to change user to VIP
        [HttpPost]
        public async Task<IActionResult> ChangeUserVIP(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.IsVip = true;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(UserManagement));
            }
            ModelState.AddModelError(string.Empty, "Error while upgrading to VIP.");
            return RedirectToAction(nameof(UserManagement));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAdmin(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Assign the admin role to the user
            var result = await _userManager.AddToRoleAsync(user, "Admin"); // Assuming "Admin" is the role name
            if (result.Succeeded)
            {
                // Optionally, return a success message or redirect
                return RedirectToAction(nameof(UserManagement));
            }

            // Handle any errors that occurred during the role assignment
            ModelState.AddModelError(string.Empty, "Error while assigning admin role.");
            return RedirectToAction(nameof(UserManagement));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(UserManagement));
            }

            ModelState.AddModelError(string.Empty, "Error while deleting user.");
            return RedirectToAction(nameof(UserManagement));
        }
    }

}
