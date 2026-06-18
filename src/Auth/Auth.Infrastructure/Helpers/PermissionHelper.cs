using Auth.Domain.Constants.Permission;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Auth.Infrastructure.Helpers
{
    public static class PermissionHelper
    {
        public static IReadOnlyList<string> GetAllPermission()
        {
            var nestedTypes = typeof(UserPermission).GetNestedTypes(BindingFlags.Public);
            return nestedTypes
            .SelectMany(t => t.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
            .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string))
            .Select(f => (string)f.GetRawConstantValue()!)
            .Distinct()
            .ToList();
        }
    }
}
