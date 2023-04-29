using Microsoft.AspNetCore.Identity;
using ParkingManagementSystem.Constants;

namespace ParkingManagementSystem.Data
{
    public class DbSeeder
    {
        //seed default data
        public static async Task SeedDefaultData(IServiceProvider service)
        {
            var userMgr = service.GetService<UserManager<IdentityUser>>();
            var roleMgr = service.GetService<RoleManager<IdentityRole>>();
            //adding some role to db
            await roleMgr.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleMgr.CreateAsync(new IdentityRole(Roles.User.ToString()));
            await roleMgr.CreateAsync(new IdentityRole(Roles.Manager.ToString()));

            //create admin user
            var admin = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            };
            var userInDb = await userMgr.FindByEmailAsync(admin.Email);
            if(userInDb is null) 
            {
                await userMgr.CreateAsync(admin, "Admin@123");
                await userMgr.AddToRoleAsync(admin, Roles.Admin.ToString());
            }

            //create manager user
            var manager = new IdentityUser
            {
                UserName = "manager@gmail.com",
                Email = "manager@gmail.com",
                EmailConfirmed = true
            };
            var userMngInDb = await userMgr.FindByEmailAsync(manager.Email);
            if (userMngInDb is null)
            {
                await userMgr.CreateAsync(manager, "Manager@123");
                await userMgr.AddToRoleAsync(manager, Roles.Manager.ToString());
            }
        }
    }
}
