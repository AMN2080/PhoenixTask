using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using PhoenixTask.Application;
using PhoenixTask.Infrastructure;
using PhoenixTask.Infrastructure.Authentication;
using PhoenixTask.Persistance;
using PhoenixTask.WebApi.Middleware;
using System.Reflection;
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Services.AddLogging(config =>
{
    config.AddNLog("Nlog.config");
});

bool runFromContainer = builder.Configuration.GetValue<bool>("RunIncontainer", false);
if (runFromContainer)
{
    #region Docker Config
    var httpsPort = builder.Configuration.GetValue("ASPNETCORE_HTTPS_PORT", 44388);
    var certPassword = "1580489e-7a6a-45e3-8b18-1d02c2ee6860";// builder.Configuration.GetValue<string>("CertPassword");
    var certPath = "localhost-dev.pfx";// builder.Configuration.GetValue<string>("CertPath");
    Console.WriteLine(certPath);
    Console.WriteLine(File.Exists(certPath));
    foreach (var item in Directory.GetFiles(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)))
    {
        Console.WriteLine(item);
    }
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.Listen(System.Net.IPAddress.Any, httpsPort, listenoption => listenoption.UseHttps(certPath, certPassword));
    });
    #endregion
}
builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly());
// Add services to the container.
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPersistence(builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddSwaggerGen(swaggerGenOptions =>
{
    swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PhoenixTask API",
        Version = "v1"
    });

    swaggerGenOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
});

builder.Services.AddAuthorization();
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorzationHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UsePersistance();

app.UseCustomExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();