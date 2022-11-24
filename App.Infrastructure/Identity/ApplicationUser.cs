using App.Domain.Entities;
using App.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace App.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<string>
    {
        public string FullName { get; set; }
        public UserType UserType { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; }
        public Guid? UserId { get; set; }
        public User User { get; set; } 
        public ICollection<ApplicationRole> Roles { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
