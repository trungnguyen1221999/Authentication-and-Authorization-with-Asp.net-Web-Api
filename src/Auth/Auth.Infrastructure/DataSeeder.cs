using Auth.Domain.Constants;
using Auth.Domain.Constants.Claims;
using Auth.Domain.Constants.Permission;
using Auth.Domain.Constants.Roles;
using Auth.Domain.Cores;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using System.Security.Claims;

namespace Auth.Infrastructure
{
    public class DataSeeder
    {
        public static async Task SeedAsync(RoleManager<AppRole> roleManager)
        {
            // Seed roles
            var roles = typeof(UserRoles)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(f => f.IsLiteral && f.FieldType == typeof(string))
                .Select(f => (string)f.GetRawConstantValue()!)
                .ToList();

            foreach (var role in roles)
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new AppRole { Name = role, RoleName = role });

            // Seed permissions 
            await SeedPermissionsAsync(roleManager, UserRoles.Moderator, [
                UserPermission.Post.View,
                UserPermission.Post.Create,
                UserPermission.Post.Edit,
                UserPermission.Post.Delete,
                UserPermission.User.View,
            
            ]);

            await SeedPermissionsAsync(roleManager, UserRoles.User, [
                UserPermission.Post.View,
                UserPermission.Post.Create,
                UserPermission.Post.Edit,
                UserPermission.Post.Delete,
            ]);
        }

        private static async Task SeedPermissionsAsync(
            RoleManager<AppRole> roleManager, string roleName, string[] permissions)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role is null) return;

            var existingClaims = await roleManager.GetClaimsAsync(role);

            foreach (var permission in permissions)
            {
                var alreadyExists = existingClaims
                    .Any(c => c.Type == UserClaims.Permission && c.Value == permission);

                if (!alreadyExists)
                    await roleManager.AddClaimAsync(role, new Claim(UserClaims.Permission, permission));
            }
        }
    }
}