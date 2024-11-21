using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Text.Encodings.Web;
using PHIASPACE.MAPPINGLISTING.DAL.Model.Db;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PHIASPACEMappingListing");
builder.Services.AddDbContext<ZamphiaMainDbContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "PHIASpace";
        options.DefaultChallengeScheme = "oidc";
    })
    .AddCookie("PHIASpace", options =>{        
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    })
    .AddOpenIdConnect("oidc", options =>
    {
        options.SignInScheme="PHIASpace";
        options.Authority = "https://studyspace.umbphia.org";
        options.SignedOutCallbackPath = "/Home/Index";
        options.ClientId = "PHIASAPCE.MAPPINGLISTING";
        options.ClientSecret = "U0c7ul3n16GgTOZze5x";
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
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
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
// builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy("PERMISSION.APP.VIEW",
//             policy => policy.Requirements.Add(new PermissionRequirement("PERMISSION.APP.VIEW")));
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
