using KairosTest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace KairosTest.Services
{
    public class DbMigrationService
    {
        public static async void SeedIt(IServiceProvider serviceProvider)
        {
            using var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            await using var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            if (context == null) return;
            await context.Database.EnsureCreatedAsync().ContinueWith(async dbCreate =>
            {
                await context.Database.MigrateAsync();
            });
            //await context.Database.EnsureCreatedAsync();
            //await context.Database.MigrateAsync();
        }
    }
}
