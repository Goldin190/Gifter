using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Gifter.Models;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartupAttribute(typeof(Gifter.Startup))]
namespace Gifter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // Create Admin role and add Example admin account
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool    
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                   

                var user = new ApplicationUser();
                user.UserName = "Admin@gmail.com";
                user.Email = "Admin@gmail.com";

                string userPWD = "Pa$$w0rd";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            // Create User role and add Example user account 
            if (!roleManager.RoleExists("User"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
                
                var user = new ApplicationUser();
                user.UserName = "User@gmail.com";
                user.Email = "User@gmail.com";

                string userPWD = "Pa$$w0rd";

                var chkUser = UserManager.Create(user, userPWD);
                
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "User");

                }

            }
        }
    }
}
