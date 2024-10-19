﻿using E_Commerce.Application.AutoMapping.ToMovieMapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.Infastructure.Install
{
	public class AutoMapperInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(ToMovieMapping));
        }
    }
}
