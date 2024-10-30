using WatchMovie.Domain.Entities.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WatchMovie.Infastructure.DbContexts;
using Microsoft.AspNetCore.Identity;
using WatchMovie.Domain.Entities.Roles;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace WatchMovie.Infastructure.Install
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
