using E_Commerce.Infastructure.Install;
using E_Commerce.Infastructure.Persistence;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("Config/appsettings.json", optional: false, reloadOnChange: true);

builder.Services.InstallServicesInAssembly(builder.Configuration);

var app = builder.Build();

DbInitializer.InitializeDatabase(app);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
