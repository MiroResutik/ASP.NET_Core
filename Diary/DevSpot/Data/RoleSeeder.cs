using DevSpot.Constants;
using Microsoft.AspNetCore.Identity;

namespace DevSpot.Data
{
    public class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManger = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManger.RoleExistsAsync(Roles.Admin))
            {
                await roleManger.CreateAsync(new IdentityRole(Roles.Admin));
            }

            if (!await roleManger.RoleExistsAsync(Roles.JobSeeker))
            {
                await roleManger.CreateAsync(new IdentityRole(Roles.JobSeeker));
            }

            if (!await roleManger.RoleExistsAsync(Roles.Employer))
            {
                await roleManger.CreateAsync(new IdentityRole(Roles.Employer));
            }
        }
    }
}
