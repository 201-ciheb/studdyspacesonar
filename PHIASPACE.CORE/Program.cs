using System.Globalization;
using System.Reflection;
using System.Security.Claims;
using System.Text.Encodings.Web;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using PHIASPACE.CORE.DAL.IService;
using PHIASPACE.CORE.DAL.Model;
using PHIASPACE.CORE.DAL.Model.DB;
using PHIASPACE.CORE.DAL.Service;
using PHIASPACE.CORE.IdentityConfiguration;
using PHIASPACE.CORE.Models;
using PHIASPACE.CORE.Utility;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PHIASPACEDbConnection");

builder.Services.AddDbContext<PhiaSpaceIdentityContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<PhiaSpaceContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddIdentity<PhiaSpaceUser, IdentityRole>(options => {
        // Password settings.
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 8;
        options.Password.RequiredUniqueChars = 1;
        // Lockout settings.
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;
        // User settings.
        options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<PhiaSpaceIdentityContext>()
    .AddDefaultTokenProviders().AddUserManager<PhiaSpaceUserManager>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name =  "PHIASPACE.GEN.ALL";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(50);
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.None;
});
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.Configure<CookieTempDataProviderOptions>(options =>
{
    options.Cookie.IsEssential = true;
});

builder.Services.AddIdentityServer(
    options =>
    {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;
        options.EmitStaticAudienceClaim = true;
    })
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(connectionString, 
            sql => sql.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(connectionString, 
            sql => sql.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name));
    })
    .AddDeveloperSigningCredential()
    .AddAspNetIdentity<PhiaSpaceUser>()
    .AddProfileService<UserProfileService>();

builder.Services.AddAuthentication()
    .AddGoogle("Google", options =>
    {
        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
        options.UserInformationEndpoint = builder.Configuration["Authentication:Google:UserInfoEndpoint"];
        options.ClaimActions.Clear();
        options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
        options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
        options.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
        options.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");
        options.ClaimActions.MapJsonKey("urn:google:profile", "link");
        options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
        options.CallbackPath = string.Format("/signin-{0}", "google");
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

builder.Services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
    {
        microsoftOptions.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
        microsoftOptions.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"];
        microsoftOptions.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"];
    });
builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowPHIASpaceUI",
            builder => builder.WithOrigins("https://localhost:44444"));//https://localhost:44413, 
    });
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowPHIASpaceUI",
        builder =>
        {
            builder.WithOrigins("https://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
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
builder.Services.AddAuthorization(options =>
{
    var serviceProvider = builder.Services.BuildServiceProvider();
    using (var scope = serviceProvider.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<PhiaSpaceContext>();
        var permissions = dbContext.PhiaPermissions.ToList();
        foreach (var permission in permissions)
        {
            options.AddPolicy("PERMISSION."+permission.Permission+".CREATE", policy =>
            policy.Requirements.Add(new PermissionRequirement("PERMISSION."+permission.Permission+".CREATE")));
            options.AddPolicy("PERMISSION."+permission.Permission+".EDIT", policy =>
                policy.Requirements.Add(new PermissionRequirement("PERMISSION."+permission.Permission+".EDIT")));
            options.AddPolicy("PERMISSION."+permission.Permission+".VIEW", policy =>
                policy.Requirements.Add(new PermissionRequirement("PERMISSION."+permission.Permission+".VIEW")));
            options.AddPolicy("PERMISSION."+permission.Permission+".DELETE", policy =>
                policy.Requirements.Add(new PermissionRequirement("PERMISSION."+permission.Permission+".DELETE")));
        }
    }
});
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IProjectService, ProjectService>();
builder.Services.AddTransient<IAppService, AppService>();
builder.Services.AddTransient<ISetUpService, SetUpService>();
builder.Services.AddTransient<IPermissionService, PermissionService>();

builder.Services.AddScoped<IProfileService, UserProfileService>();   
builder.Services.AddScoped<IAuthorizationHandler, PermissionHandler>();

builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();
builder.Services.AddOptions();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    //app.UseHttpsRedirection();
}
var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "PHIASpaceDocuments")),
    RequestPath = "/PHIASpaceDocuments"
});
app.UseCors("AllowPHIASpaceUI");
app.UseRouting();
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
// using (var serviceProvider = builder.Services.BuildServiceProvider())
// {
//     using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
//     {
//         var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<PhiaSpaceUser>>();
//         var superadmin = userMgr.FindByNameAsync("superadmin").Result;
//         if (superadmin == null)
//         {
//             superadmin = new PhiaSpaceUser { Email = "superadmin@phiaspace.org", UserName = "superadmin"};
            
//             var result = userMgr.CreateAsync(superadmin, "Pass123$").Result;
//             if (!result.Succeeded)
//             {
//                 throw new Exception(result.Errors.First().Description);
//             }
//             result = userMgr.AddClaimsAsync(superadmin, new Claim[]{
//                 new Claim(JwtClaimTypes.Name, "Super Admin"),
//                 new Claim(JwtClaimTypes.GivenName, "Super"),
//                 new Claim(JwtClaimTypes.FamilyName, "Admin")
//             }).Result;
//             if (!result.Succeeded)
//             {
//                 throw new Exception(result.Errors.First().Description);
//             }
//         }
//     }
// }

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
    {
        serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

        var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
        // context.Database.Migrate();
        // if (!context.Clients.Any())
        // {
            // foreach (var client in Clients.Get())
            // {
            //     context.Clients.Add(client.ToEntity());
            // }
            // context.SaveChanges();
        // }

        // if (!context.IdentityResources.Any())
        // {
        //     foreach (var resource in Resources.GetIdentityResources())
        //     {
        //         context.IdentityResources.Add(resource.ToEntity());
        //     }
        //     context.SaveChanges();
        // }

        // if (!context.ApiScopes.Any())
        // {
            // foreach (var resource in Scopes.GetApiScopes())
            // {
            //     context.ApiScopes.Add(resource.ToEntity());
            // }
            // context.SaveChanges();
        // }

        // foreach (var resource in Resources.GetApiResources()){
        //     context.ApiResources.Add(resource.ToEntity());
        // }
        // context.SaveChanges();
    }

app.Run();