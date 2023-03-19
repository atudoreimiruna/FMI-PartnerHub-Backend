using Microsoft.AspNetCore.Identity;
using Licenta.Core.Entities;

namespace Licenta.Infrastructure.Seeders;

public class DataSeeder
{
    private readonly RoleManager<Role> _roleManager;

    public DataSeeder(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async void CreateRoles()
    {
        string[] roleNames = {
                            "Admin",
                            "User"
                            };

        foreach (var roleName in roleNames)
        {
            var role = new Role
            {
                Name = roleName
            };
            _roleManager.CreateAsync(role).Wait();
        }
    }
}
