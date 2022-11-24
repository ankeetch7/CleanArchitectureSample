using App.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace App.Infrastructure.Identity
{
    public class ApplicationRole : IdentityRole<string>
    {
        public RoleType RoleType { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
