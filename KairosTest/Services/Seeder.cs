using KairosTest.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace KairosTest.Services
{
    public static class Seeder
    {
        public static async void SeedIt(IServiceProvider serviceProvider)
        {
            using var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            await using var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            //context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            if (context == null) return;
            context.Database.EnsureCreated();
            context.Database.Migrate();
            if (await context.Users.AnyAsync()) return;
            var userManag = serviceScope.ServiceProvider.GetService<UserManager<IdentityUser>>();
            var user = new IdentityUser { UserName = "admin", Email = "admin@email.com" };
            var result = await userManag.CreateAsync(user, "admin");
            if (result.Succeeded)
            {
                var resRol = await userManag.AddToRoleAsync(user, "Admin");
                if (resRol.Succeeded)
                {
                    user = new IdentityUser { UserName = "user1", Email = "user1@email.com" };
                    result = await userManag.CreateAsync(user, "user1");
                    if (result.Succeeded)
                    {
                        resRol = await userManag.AddToRoleAsync(user, "Penyewa");
                        if (resRol.Succeeded)
                        {
                            userManag.Dispose();
                            serviceScope.Dispose();
                            context.Dispose();
                        }
                    }
                }
            }
        }
    }
}
