using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace App.Domain.Enums
{
    public enum Gender
    {
        [Display(Name = "Male")]
        Male = 1,
        [Display(Name = "Male")]
        Female = 2
    }
}
