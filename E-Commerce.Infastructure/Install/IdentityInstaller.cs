using E_Commerce.Domain.Entities.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using E_Commerce.Infastructure.DbContexts;
using Microsoft.AspNetCore.Identity;
using E_Commerce.Domain.Entities.Roles;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace E_Commerce.Infastructure.Install
{
    public class IdentityInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>() 
            .AddDefaultTokenProviders();
        }
    }

}
