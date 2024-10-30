using WatchMovie.Application.AutoMapping.ToMovieMapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WatchMovie.Infastructure.Install
{
	public class AutoMapperInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(ToMovieMapping));
        }
    }
}
