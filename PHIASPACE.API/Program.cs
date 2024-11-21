using IdentityServer4.AccessTokenValidation;
using Microsoft.OpenApi.Models;
using PHIASPACE.API.IService;
using PHIASPACE.API.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Title = builder.Configuration["APITitle"],
        Description = builder.Configuration["APIDescription"],
        //TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact {
            Name = "201 Development Team",
            //Email = "xyz@gmail.com",
            //Url = new Uri("https://example.com"),
        },
        // License = new OpenApiLicense {
        //     Name = "Use under OpenApiLicense",
        //         Url = new Uri("https://example.com/license"),
        // }
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = @"Enter your access_token in the text input below. Example: 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9....'",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
.AddIdentityServerAuthentication(options =>
{
    options.ApiName = "PHIASAPCE.API";
    options.RequireHttpsMetadata = false; 
    options.Authority = "https://localhost:7280";
    //g7yFePKZ6R6GwQeu
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "PHIASAPCE.API.Read");
    });
});
builder.Services.AddHttpClient<IPermissionService, PermissionService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7280");
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.RoutePrefix = String.Empty;
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "StudySpace API V1");
    options.DocumentTitle = "StudySpace API UI Doc.";
});
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
