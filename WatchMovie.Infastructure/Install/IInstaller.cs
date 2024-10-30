using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace WatchMovie.Infastructure.Install
{
	internal interface IInstaller
	{
		void InstallServices(IServiceCollection services, IConfiguration configuration);
	}
}
