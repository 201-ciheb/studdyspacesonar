using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using PHIASPACE.LINKAGE.DAL.IService;
using PHIASPACE.LINKAGE.DAL.Model;
using PHIASPACE.LINKAGE.DAL.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AimsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AimsDbConnection")));

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

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddMvc();

builder.Services.AddScoped<IDbProvider, DbProvider>();
builder.Services.AddScoped<ILinkageService, LinkageService>();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
