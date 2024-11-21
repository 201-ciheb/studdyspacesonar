using System.Globalization;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PHIASPACE.INTERFACE.DAL.IService;
using PHIASPACE.INTERFACE.DAL.Model.Data;
using PHIASPACE.INTERFACE.DAL.Service;
using PHIASPACE.INTERFACE.Utils;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("InterfaceDbConnection") ?? throw new InvalidOperationException("Connection string 'InterfaceDbConnection' not found.");
builder.Services.AddDbContext<InterfaceDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "PHIASpace";
        options.DefaultChallengeScheme = "oidc";
    })
    .AddCookie("PHIASpace", options =>{        
        options.ExpireTimeSpan = TimeSpan.FromMinutes(50);
    })
    .AddOpenIdConnect("oidc", options =>
    {
        options.SignInScheme="PHIASpace";
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
        options.ClaimActions.MapJsonKey("role", "role");
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

builder.Services.AddSession();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(
    opts =>
	{
        var supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("en-US"),
            new CultureInfo("pt-PT")
        };
        opts.DefaultRequestCulture = new RequestCulture("en-US");
        opts.SupportedCultures = supportedCultures;
	    opts.SupportedUICultures = supportedCultures;
	}); 

builder.Services.AddHttpClient("KoboDataService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["API:KoboDataService:BaseUrl"]);
    client.Timeout = TimeSpan.FromSeconds(int.Parse(builder.Configuration["API:KoboDataService:TimeoutInSeconds"]));
});

builder.Services.AddScoped<ISatLabService, SatLabService>();

// builder.Services.AddHangfire(config => config
//     .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
//     .UseSimpleAssemblyNameTypeSerializer()
//     .UseRecommendedSerializerSettings()
//     .UseSqlServerStorage(builder.Configuration.GetConnectionString("InterfaceDbConnection"), new SqlServerStorageOptions
//     {
//         SchemaName = "schedule"
//     })
// );

// builder.Services.AddHangfireServer();
// Schedule the kobo data pull hourly

// using (var serviceProvider = builder.Services.BuildServiceProvider())
// {
//     var _httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
//     var _satLabService = serviceProvider.GetRequiredService<ISatLabService>();
//     var recurringJobManager = serviceProvider.GetRequiredService<IRecurringJobManager>();
//     var formId = builder.Configuration.GetSection("API:KoboDataService:FormId").Get<string[]>().FirstOrDefault();
//     HttpUtils utils = new HttpUtils(_httpClientFactory, builder.Configuration, _satLabService);
//     RecurringJob.AddOrUpdate("PullDataFromKobo", () => utils.GetDataFromKoboCollectAPI(formId), Cron.Hourly); 
// }
 
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
#if !DEBUG
app.UseHangfireDashboard("/schedule", new DashboardOptions
{
    Authorization = new[] { new PhiaSpaceAuthFilter() }
});
#endif
//RecurringJob.AddOrUpdate<MyJob>("jobId", job => job.Execute(), Cron.Minutely);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
