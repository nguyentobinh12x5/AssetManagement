using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Application.Common.Extensions
{
    public static class PasswordGenerator
    {
        public static string GeneratePassword(this string username, DateTime dateOfBirth)
        {
            return $"{char.ToUpper(username[0])}{username[1..]}@{dateOfBirth:ddMMyyyy}";
        }
    }
}