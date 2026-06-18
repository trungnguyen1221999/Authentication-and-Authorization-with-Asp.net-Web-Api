using Auth.Application.Services;
using Auth.Domain.Constants.Roles;
using Auth.Domain.Cores;
using Auth.Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Infrastructure.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public PermissionService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<HashSet<string>?> GetPermissionsForUserAsync(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var permissions = new HashSet<string>();
            if (roles.Contains(UserRoles.Admin))
            {
                permissions = PermissionHelper.GetAllPermission().ToHashSet();
                return permissions;
            }
            
            foreach (var roleName in roles)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                var claims = await _roleManager.GetClaimsAsync(role!);
                var roleClaimValue = claims.Select(r => r.Value).ToList();
                permissions.UnionWith(roleClaimValue);
            }

            return permissions;
        }
    }
}
