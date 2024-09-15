using E_Commerce.Infastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace E_Commerce.Infastructure.Persistence
{
	public static class DbInitializer
	{
		public static void InitializeDatabase(IHost app)
		{
			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				var context = services.GetRequiredService<ApplicationDbContext>();
				context.Database.Migrate();
			}
		}
	}
}
