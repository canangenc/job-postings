using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using WebSite.Models;
using Owin;
using WebSite.Controllers;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(WebSite.Startup))]
namespace WebSite
{
    public partial class Startup
    {
        ApplicationDbContext db = new ApplicationDbContext();

        

        //Create Admin Account...
        public void createUser()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var admin = db.Users.Where(x => x.Email == "MyKitchen@gmail.com").FirstOrDefault();
            if (admin == null)
            {
                var user = new ApplicationUser()
                {
                    Email = "WebSite@gmail.com",
                    UserName = "website123",
                    fullName = "website Admin"
                };

                var check = userManager.Create(user, "123456");
                if (check.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }

        }

        //Create roles...
        public void createRoles()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            IdentityRole role;
            if (!roleManager.RoleExists("Admin"))
            {
                role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

            }
            if (!roleManager.RoleExists("User"))
            {
                role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);

            }

        }


    }
}
