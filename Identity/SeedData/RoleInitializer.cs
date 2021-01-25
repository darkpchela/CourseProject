using Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Identity.SeedData
{
    public class RoleInitializer
    {
        public async Task InitializeAsync(UserManager<AppUser> userManager, RoleManager<Role> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string password = "qwerty";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new Role("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new Role("user"));
            }
            if (await roleManager.FindByNameAsync("blocked") == null)
            {
                await roleManager.CreateAsync(new Role("blocked"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                AppUser admin = new AppUser { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}