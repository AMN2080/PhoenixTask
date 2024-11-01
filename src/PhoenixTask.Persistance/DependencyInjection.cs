using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Domain.Projects;
using PhoenixTask.Domain.Tasks;
using PhoenixTask.Domain.Users;
using PhoenixTask.Domain.Workspaces;
using PhoenixTask.Persistance.Infrastructure;
using PhoenixTask.Persistance.Repositories;

namespace PhoenixTask.Persistance;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString(ConnectionString.SettingsKey)!;
        bool.TryParse(configuration.GetSection("UseInMemoryDatabase")?.Value, out bool isInmemorydb);
        services.AddSingleton(new ConnectionString(connectionString,isInmemorydb));
        if (isInmemorydb)
        {
            services.AddDbContext<PhoenixDbContext>(options => options.UseInMemoryDatabase("Default"));
        }
        else
        {
            services.AddDbContext<PhoenixDbContext>(options => options.UseSqlServer(connectionString));
        }

        services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<PhoenixDbContext>());

        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<PhoenixDbContext>());

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IWorkSpaceRepository, WorkSpaceRepository>();

        services.AddScoped<IProjectRepository, ProjectRepository>();

        services.AddScoped<IBoardRepository, BoardRepository>();

        services.AddScoped<IWorkSpaceMemberRepository, WorkSpaceMemberRepository>();

        services.AddScoped<IProjectMemberRepository, ProjectMemberRepository>();

        services.AddScoped<ITaskRepository, TaskRepository>();

        services.AddScoped<ISettingRepository, SettingRepository>();

        return services;
    }
    public static IApplicationBuilder UsePersistance(this IApplicationBuilder app)
    {
        using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

        using PhoenixDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<PhoenixDbContext>();
        ConnectionString connectionString = serviceScope.ServiceProvider.GetRequiredService<ConnectionString>();

        if (!connectionString.InMemoryDb)
        {
            dbContext.Database.Migrate();
        }

        return app;
    }
}
