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
            string trimmedUsername = username.Trim();
            return $"{char.ToUpper(trimmedUsername[0])}{trimmedUsername[1..]}@{dateOfBirth:ddMMyyyy}";
        }
    }
}