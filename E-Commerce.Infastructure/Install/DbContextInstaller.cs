using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.Infastructure.Install
{
	public class DbContextInstaller : IInstaller
	{
		public void InstallServices(IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<DbContexts.ApplicationDbContext>(options =>
						options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
		}
	}
}
