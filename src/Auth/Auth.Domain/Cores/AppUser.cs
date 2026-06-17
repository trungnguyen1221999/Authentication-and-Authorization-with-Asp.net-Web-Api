using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Auth.Domain.Cores
{
    [Table("AppUsers")]
    public class AppUser : IdentityUser<Guid>
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        public bool isActive { get; set; } = false;

        public RoleName Role { get; set; } = RoleName.User;
    }
}

public enum RoleName
{
    Admin = 1,
    Moderator = 2,
    User = 3
}