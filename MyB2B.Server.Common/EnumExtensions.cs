using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace MyB2B.Server.Common
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue) => enumValue.GetType().GetMember(enumValue.ToString()).First().GetCustomAttribute<DisplayAttribute>().Name;
    }
}