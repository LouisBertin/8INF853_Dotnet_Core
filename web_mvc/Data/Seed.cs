using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using web_mvc.Models;

namespace web_mvc.Data
{
    public class Seed
    {

        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration Configuration)
        {
            //initializing custom roles   
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = { "Admin", "Employee", "Customer" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1  
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

     

            //creating a super user who could maintain the web app
            var poweruser = new IdentityUser
            {
                UserName = Configuration.GetSection("UserSettings")["UserEmail"],
                Email = Configuration.GetSection("UserSettings")["UserEmail"]
            };

            string UserPassword = Configuration.GetSection("UserSettings")["UserPassword"];
            var _user = await UserManager.FindByEmailAsync(Configuration.GetSection("UserSettings")["UserEmail"]);

            if (_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(poweruser, UserPassword);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the "Admin" role 
                    await UserManager.AddToRoleAsync(poweruser, "Admin");
                }
            }

            var employeeUser = new IdentityUser
            {
                UserName = "employee@employee.com",
                Email = "employee@employee.com"
            };

            string employeePassword = "Employee@123";
            var _employeeUser = await UserManager.FindByEmailAsync("employee@employee.com");

            if (_employeeUser == null)
            {
                var createEmployeeUser = await UserManager.CreateAsync(employeeUser, employeePassword);
                if (createEmployeeUser.Succeeded)
                {
                    //here we tie the new user to the "Admin" role 
                    await UserManager.AddToRoleAsync(employeeUser, "Employee");

                }
            }

            var customerUser = new IdentityUser
            {
                UserName = "customer@customer.com",
                Email = "customer@customer.com"
            };

            string customerPassword = "Customer@123";
            var _customerUser = await UserManager.FindByEmailAsync("customer@customer.com");

            if (_customerUser == null)
            {
                var createCustomerUser = await UserManager.CreateAsync(customerUser, customerPassword);
                if (createCustomerUser.Succeeded)
                {
                    //here we tie the new user to the "Admin" role 
                    await UserManager.AddToRoleAsync(customerUser, "Customer");

                }
            }
        }

        public static async Task CreateCategories(IServiceProvider serviceProvider, IConfiguration Configuration)
        {
            // get context service
            ApplicationDbContext context = serviceProvider.GetService<ApplicationDbContext>();

            if (context.Categorie.Any())
            {
                return;   // DB has been seeded
            }

            var categorie = new Categorie("test");
            context.Update(categorie);
            await context.SaveChangesAsync();
        }

        public static async Task CreateMarque(IServiceProvider serviceProvider, IConfiguration Configuration)
        {
            // get context service
            ApplicationDbContext context = serviceProvider.GetService<ApplicationDbContext>();

            if (context.Marque.Any())
            {
                return;   // DB has been seeded
            }

            var marque = new Marque("funko");
            context.Update(marque);
            await context.SaveChangesAsync();
        }

    }
}
