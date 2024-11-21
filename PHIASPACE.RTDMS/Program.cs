using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using PHIASPACE.RTDMS.DAL.IService;
using PHIASPACE.RTDMS.DAL.Service;
using PHIASPACE.RTDMS.Helpers;
using PHIASPACE.RTDMS.DAL.MssqlDbService;
using PHIASPACE.RTDMS.DAL.MysqlDbService;
using PHIASPACE.RTDMS.DAL.DbProviderFactory;
using PHIASPACE.RTDMS.Utility;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AimsMssqlDbConnection")
                      ?? throw new InvalidOperationException("Connection string 'RtdmsDbConnection' not found.");

builder.Services.AddDbContext<AimsMssqlDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AimsMssqlDbConnection")));

builder.Services.AddDbContext<AimsMysqlDbContext>(options =>
         options.UseMySql(builder.Configuration.GetConnectionString("AimsMysqlDbConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("AimsMysqlDbConnection"))));

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "PHIASpace";
    options.DefaultChallengeScheme = "oidc";
})
.AddCookie("PHIASpace", options => {
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
})
.AddOpenIdConnect("oidc", options =>
{
    options.SignInScheme = "PHIASpace";
    options.Authority = builder.Configuration["Authentication:PHIASpace:Authority"];
    options.SignedOutCallbackPath = "/Home/Index";
    options.ClientId = builder.Configuration["Authentication:PHIASpace:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:PHIASpace:ClientSecret"];
    options.ResponseType = "code";
    options.ResponseMode = "query";
    options.GetClaimsFromUserInfoEndpoint = true;
    options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
    options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
    options.ClaimActions.MapJsonKey("phone_number", "phone_number");
    options.ClaimActions.MapJsonKey("preferred_username", "preferred_username");
    options.ClaimActions.MapJsonKey(ClaimTypes.Role, "role");
    options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
    options.ClaimActions.MapJsonKey("permission", "permission");
    options.ClaimActions.MapJsonKey("projects", "projects");
    options.SaveTokens = true;
    options.Events.OnRemoteFailure = ctx =>
    {
        string message = UrlEncoder.Default.Encode(ctx.Failure.Message);
        if (!string.IsNullOrEmpty(message) && message.Length > 1500)
        {
            message = message.Substring(0, 1499);
        }
        ctx.Response.Redirect("/" + message);
        ctx.HandleResponse();
        return Task.FromResult(0);
    };
});

// Add services to the container
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddMvc();

builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IAIssueService, AIssueService>();
builder.Services.AddScoped<IRecordService, RecordService>();
builder.Services.AddScoped<ICAPIService, CAPIService>();
builder.Services.AddScoped<IIssueService, IssueService>();
builder.Services.AddScoped<IMonitoringService, MonitoringService>();
builder.Services.AddScoped<IUtilService, UtilService>();
builder.Services.AddScoped<IFieldCheckService, FieldCheckService>();

builder.Services.AddScoped<TreeHelper>(); 
builder.Services.AddScoped<IZoneService, ZoneService>();
builder.Services.AddScoped<IHouseholdService, HouseholdService>();
builder.Services.AddScoped<IFormDataService, FormDataService>();

builder.Services.AddScoped<ZoneService>();
builder.Services.AddScoped<UserHelper>();
builder.Services.AddHttpContextAccessor();

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(builder.Configuration);
});

// Use appropriate provider based on configuration
var dbType = builder.Configuration["DatabaseType:DbType"];
switch (dbType)
{
    case "MSSQL":
        builder.Services.AddScoped<IAimsDbProvider, MssqlDbProvider>();
        break;
    case "MySQL":
        builder.Services.AddScoped<IAimsDbProvider, MysqlDbProvider>();
        break;
    default:
        throw new NotSupportedException($"Database type {dbType} is not yet supported.");
}

// Build the app
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
