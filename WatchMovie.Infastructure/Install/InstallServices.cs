using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace WatchMovie.Infastructure.Install
{
	public static class InstallServices
	{
		public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
		{
			var executingAssembly = Assembly.GetExecutingAssembly();

			var installers = executingAssembly.ExportedTypes
				.Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
				.Select(Activator.CreateInstance)
				.Cast<IInstaller>()
				.ToList();

			installers.ForEach(installer => installer.InstallServices(services, configuration));
		}
	}
}
