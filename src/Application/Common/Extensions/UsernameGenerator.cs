using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Application.Common.Extensions
{
    public static class UsernameGenerator
    {
        public static string GenerateUsername(this IEnumerable<string?> existingUsernames, string firstName, string lastName)
        {
            string FirstName = firstName.ToLowerInvariant();

            string[] lastNameParts = lastName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string LastName = string.Concat(lastNameParts.Select(part => part[..1].ToLowerInvariant()));


            string baseUsername = $"{firstName}{LastName}";

            int suffix = 0;

            string newUsername = baseUsername;

            while (existingUsernames.Contains(newUsername))
            {
                suffix++;
                newUsername = $"{baseUsername}{suffix}";
            }

            return newUsername;
        }
    }
}
