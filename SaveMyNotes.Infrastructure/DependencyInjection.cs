using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SaveMyNotes.Application.Common.Interfaces;
using SaveMyNotes.Infrastructure.Persistence.Context;
using SaveMyNotes.Infrastructure.Persistence.Repositories;
using SaveMyNotes.Infrastructure.Services.AI;
using SaveMyNotes.Infrastructure.Services.Identity;

namespace SaveMyNotes.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext & PostgreSQL
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString,
                builder => builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        // UnitOfWork
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<AppDbContext>());

        IServiceCollection serviceCollection = services.AddHttpClient();

        // Repository & Services
        services.AddScoped<INoteRepository, NoteRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ITagRepository, TagRepository>();

        // AIService artık IConfiguration ve IHttpClientFactory bekliyor
        services.AddScoped<IAIService, AIService>();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}