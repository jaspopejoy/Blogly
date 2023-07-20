using Blogly.Data;
using Blogly.Enums;
using Blogly.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blogly.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BlogUser> _userManager;

        public DataService(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<BlogUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task ManageDataAsync()
        {
            //Task: create the Database from the Migrations
            await _dbContext.Database.MigrateAsync();

            //Task 1: seed a few roles into the system
            await SeedRolesAsync();

            //Task 2: seed a few users into the system
            await SeedUsersAsync();
        }

        private async Task SeedRolesAsync()
        {
            //If roles already exist, do nothing
            if (_dbContext.Roles.Any())
            {
                return;
            }

            //Otherwise Create Roles
            foreach (var role in Enum.GetNames(typeof(BlogRole)))
            {
                //Use a role manager to create roles
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private async Task SeedUsersAsync()
        {
            //If Users already exist, do nothing
            if (_dbContext.Users.Any())
            {
                return;
            }

            //Step 1: Creates a new instance of BlogUser
            var adminUser = new BlogUser()
            {
                Email = "cyphre187@gmail.com",
                UserName = "cyphre187@gmail.com",
                FirstName = "Jason",
                LastName = "Popejoy",
                EmailConfirmed = true
            };

            //Step 2: Use the UserManager to create a new user that is defined 
            await _userManager.CreateAsync(adminUser, "Abc&123!");

            //Step 3: add new user to the Administrator role
            await _userManager.AddToRoleAsync(adminUser, BlogRole.Administrator.ToString());

            var modUser = new BlogUser()
            {
                Email = "nytfury77@yahoo.com",
                UserName = "nytfury77@yahoo.com",
                FirstName = "Jason",
                LastName = "Popejoy",
                EmailConfirmed = true
            };

            //Step 2: Use the UserManager to create a new user that is defined 
            await _userManager.CreateAsync(modUser, "Abc&123!");

            //Step 3: add new user to the Administrator role
            await _userManager.AddToRoleAsync(modUser, BlogRole.Moderator.ToString());
        }

    }
}
