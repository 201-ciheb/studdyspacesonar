using System.Globalization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
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
        options.Authority = "https://localhost:7280";
        options.SignedOutCallbackPath = "/Home/Index";
        options.ClientId = "PHIASAPCE.TICKETING";
        options.ClientSecret = "g7yFePKZ6R6GwQeu";
        options.ResponseType = "code";
        options.ResponseMode = "query";
        //options.Scope.Add("PHIASAPCE.TICKETING.Write");
        options.Scope.Add("role");
        options.GetClaimsFromUserInfoEndpoint = true;
        options.SaveTokens = true;
    });
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
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
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
