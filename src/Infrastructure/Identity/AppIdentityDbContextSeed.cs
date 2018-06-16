using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roleAdmin = new IdentityRole();
            roleAdmin.Name = "Admin";
            await roleManager.CreateAsync(roleAdmin);

            var roleUser = new IdentityRole();
            roleUser.Name = "User";
            await roleManager.CreateAsync(roleUser);

            var defaultUser = new ApplicationUser { UserName = "demouser@microsoft.com", Email = "demouser@microsoft.com" };
            await userManager.CreateAsync(defaultUser, "Pass@word1");
            await userManager.AddToRoleAsync(defaultUser, "User");

            var adminUser = new ApplicationUser { UserName = "admin@admin.com", Email = "admin@admin.com" };
            await userManager.CreateAsync(adminUser, "Admin!1");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
