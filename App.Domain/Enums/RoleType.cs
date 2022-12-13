using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace App.Domain.Enums
{
    public enum RoleType
    {
        [Display(Name = "Admin")]
        Admin = 1,
        [Display(Name = "User")]
        User = 2
    }
}
