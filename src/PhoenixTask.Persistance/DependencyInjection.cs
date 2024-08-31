﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Domain.Projects;
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

        services.AddSingleton(new ConnectionString(connectionString));

        // services.AddDbContext<PhoenixDbContext>(options =>options.UseInMemoryDatabase("Test"));
        services.AddDbContext<PhoenixDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<PhoenixDbContext>());

        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<PhoenixDbContext>());

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IWorkSpaceRepository, WorkSpaceRepository>();

        services.AddScoped<IProjectRepository, ProjectRepository>();

        return services;
    }
    public static IApplicationBuilder UsePersistance(this IApplicationBuilder app)
    {
        using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

        using PhoenixDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<PhoenixDbContext>();

        dbContext.Database.Migrate();

        return app;
    }
}
