using Auth.Domain.Auth;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Infrastructure.Authorization
{
    public class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        private readonly DefaultAuthorizationPolicyProvider _fallback;

        public PermissionPolicyProvider (DefaultAuthorizationPolicyProvider fallback)
        {
            _fallback = fallback;
        }
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return _fallback.GetDefaultPolicyAsync();
        }

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        {
            return _fallback.GetFallbackPolicyAsync();
        }

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                         .AddRequirements (new PermissionRequirement(policyName))
                                                         .Build();
            return Task.FromResult<AuthorizationPolicy?>(policy);
        }
    }
}
