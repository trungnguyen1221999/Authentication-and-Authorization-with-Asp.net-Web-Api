using Auth.Domain.Auth;
using Auth.Domain.Constants.Claims;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Infrastructure
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var userPermissions = context.User.Claims
                                         .Where(c => c.Type == UserClaims.Permission)
                                         .Select (c => c.Value).ToHashSet();
            if (userPermissions.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
