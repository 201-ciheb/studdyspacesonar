using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PHIASPACE.INCIDENCE.DAL.IService;
using PHIASPACE.INCIDENCE.DAL.Service;
using PHIASPACE.INCIDENCE.DAL.MysqlDbService;
using PHIASPACE.INCIDENCE.DAL.MssqlDbService;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AimsMysqlDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("AimsMysqlDbConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("AimsMysqlDbConnection"))));

builder.Services.AddDbContext<AimsMssqlDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AimsMssqlDbConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

//services
builder.Services.AddScoped<IZoneService, ZoneService>();
builder.Services.AddScoped<IIncidenceService, IncidenceService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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