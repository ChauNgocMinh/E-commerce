using WatchMovie.Domain.Entities.Movies;
using WatchMovie.Domain.Entities.Roles;
using WatchMovie.Domain.Entities.Users;
using WatchMovie.Domain.Interfaces;
using WatchMovie.Infastructure.DbContexts;
using WatchMovie.Infastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WatchMovie.Infastructure.Install
{
    public class RepoInstall : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IGenericRepository<Movie>, GenericRepository<Movie>>();
            services.AddScoped<IGenericRepository<MovieImage>, GenericRepository<MovieImage>>();
            services.AddScoped<IGenericRepository<MovieCategory>, GenericRepository<MovieCategory>>();
            services.AddScoped<IGenericRepository<MovieFeedBack>, GenericRepository<MovieFeedBack>>();
            services.AddScoped<UserService>();

        }
    }
}
