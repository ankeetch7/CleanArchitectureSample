using App.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Command.ApplicationUser
{
    public class AppUser
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public UserType UserType { get; set; }
        public string CustomerId { get; set; }
    }
    public class AuthenticateRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
