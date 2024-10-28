using E_Commerce.Domain.Entities.Movies;
using E_Commerce.Domain.Entities.Roles;
using E_Commerce.Domain.Entities.Users;
using E_Commerce.Domain.Interfaces;
using E_Commerce.Infastructure.DbContexts;
using E_Commerce.Infastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infastructure.Install
{
    public class RepoInstall : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IGenericRepository<Movie>, GenericRepository<Movie>>();
            services.AddScoped<IGenericRepository<MovieCategory>, GenericRepository<MovieCategory>>();
            services.AddScoped<UserService>();

        }
    }
}
