using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;

using static IdentityServer4.Config;

namespace IdentityServer4.Infrastructure;
public static class Extensions
{
    public static async void InitializeDatabaseAsync(this IApplicationBuilder app)
    {
        await using var serviceScope = app.ApplicationServices.CreateAsyncScope();

        var configurationDbContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
        await configurationDbContext.Database.MigrateAsync();

        if (!configurationDbContext.Clients.Any())
        {
            await configurationDbContext.Clients.AddRangeAsync(Clients.Select(c => c.ToEntity()));
            await configurationDbContext.SaveChangesAsync();
        }

        if (!configurationDbContext.IdentityResources.Any())
        {
            await configurationDbContext.IdentityResources.AddRangeAsync(IdentityResources.Select(r => r.ToEntity()));
            await configurationDbContext.SaveChangesAsync();
        }

        if (!configurationDbContext.ApiScopes.Any())
        {
            await configurationDbContext.ApiScopes.AddRangeAsync(ApiScopes.Select(s => s.ToEntity()));
            await configurationDbContext.SaveChangesAsync();
        }
    }
}
