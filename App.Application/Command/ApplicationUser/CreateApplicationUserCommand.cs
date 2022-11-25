using App.Domain.Enums;

namespace App.Application.Command.ApplicationUser
{
    public class CreateApplicationUserCommand
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public bool IsActive { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Image { get; set; }
    }
}
