using App.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Services
{
    public interface ICurrentUserService
    {
        public string UserId { get; }
        public string CustomerId { get; }
        public string UserName { get; }
        public string UserType { get; }
    }
}
