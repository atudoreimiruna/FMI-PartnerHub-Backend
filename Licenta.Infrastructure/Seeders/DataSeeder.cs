using Microsoft.AspNetCore.Identity;
using Licenta.Core.Entities;
using Licenta.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace Licenta.Infrastructure.Seeders;

public class DataSeeder
{
    private readonly RoleManager<Role> _roleManager;
    private readonly AppDbContext _context;

    public DataSeeder(RoleManager<Role> roleManager, AppDbContext context)
    {
        _roleManager = roleManager;
        _context = context;
    }

    public async void CreateRoles()
    {
        var roles = Enum.GetNames(typeof(RolesEnum)).ToList();

        foreach (var roleName in roles)
        {
            var role = new Role
            {
                Name = roleName
            };
            _roleManager.CreateAsync(role).Wait();
        }
    }
}
