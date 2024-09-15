using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace E_Commerce.Infastructure.Install
{
	internal interface IInstaller
	{
		void InstallServices(IServiceCollection services, IConfiguration configuration);
	}
}
