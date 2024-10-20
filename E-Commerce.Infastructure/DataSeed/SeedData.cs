using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using E_Commerce.Domain.Entities.Roles;

namespace E_Commerce.Infastructure.DataSeed
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new Role { Name = roleName });
                }
            }
        }

    }
}
