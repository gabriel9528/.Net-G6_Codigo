using Identity_MVC.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity_MVC.Utils
{
    public static class RoleInitializer
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            string[] roleNames = { "Admin", "User", "Client" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if(!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //Crear un usuario Admin si no existe
            string adminEmail = "admin@gmail.com";
            string adminPassword = "Admin123#";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if(adminUser == null)
            {
                var newUser = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Nombres = "Administrador"
                };

                var result = await userManager.CreateAsync(newUser, adminPassword);

                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, "Admin");
                }
            }

        }
    }
}
