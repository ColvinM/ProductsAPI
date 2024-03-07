using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Enums;

namespace ProductsAPI.Models
{
    public class DataUtility
    {
        public static async Task ManageDataAsync(IServiceProvider svcProvider)
        {
            //Service: An instance of RoleManager
            var dbContextSvc = svcProvider.GetRequiredService<ApplicationDbContext>();
            //Service: An instance of RoleManager
            var roleManagerSvc = svcProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //Service: An instance of the UserManager
            var userManagerSvc = svcProvider.GetRequiredService<UserManager<IdentityUser>>();
            //Migration: This is the programmatic equivalent to Update-Database
            //await dbContextSvc.Database.MigrateAsync();

            await SeedRolesAsync(roleManagerSvc);
            await SeedDefaultUsersAsync(userManagerSvc);

        }

        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            //await roleManager.CreateAsync(new IdentityRole(nameof(Roles.Admin)));
            await roleManager.CreateAsync(new IdentityRole(nameof(Roles.Manager)));
            await roleManager.CreateAsync(new IdentityRole(nameof(Roles.User)));
        }

        public static async Task SeedDefaultUsersAsync(UserManager<IdentityUser> userManager)
        {
            // Seed Default Manager User
            var managerUser = new IdentityUser
            {
                UserName = "manager@mailinator.com",
                Email = "manager@mailinator.com"
            };

            try
            {
                var user = await userManager.FindByEmailAsync(managerUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(managerUser, "Abc&123!");
                    await userManager.AddToRoleAsync(managerUser, nameof(Roles.Manager));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("*************  ERROR  *************");
                Console.WriteLine("Error Seeding Default Manager User.");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************************");
                throw;
            }

            // Seed Default User
            var defaultUser = new IdentityUser
            {
                UserName = "user@mailinator.com",
                Email = "user@mailinator.com"
            };

            try
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Abc&123!");
                    await userManager.AddToRoleAsync(defaultUser, nameof(Roles.User));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("*************  ERROR  *************");
                Console.WriteLine("Error Seeding Default User.");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************************");
                throw;
            }

        }
    }
}
