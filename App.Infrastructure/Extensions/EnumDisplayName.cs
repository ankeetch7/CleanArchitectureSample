using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Extensions
{
    public static class EnumDisplayName
    {
        public static string GetEnumDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                        .GetMember(enumValue.ToString())
                        .First()
                        .GetCustomAttribute<DisplayAttribute>().Name;
        }
    }
}
