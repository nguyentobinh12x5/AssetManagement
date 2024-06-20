using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AssetManagement.Application.Common.Extensions
{
    public static class UsernameGenerator
    {
        public static string GenerateUsername(this IEnumerable<string?> existingUsernames, string firstName, string lastName)
        {
            string normalizedFirstName = NormalizeAndRemoveDiacritics(firstName).ToLowerInvariant();
            string normalizedLastName = NormalizeAndRemoveDiacritics(lastName);

            string[] lastNameParts = normalizedLastName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string lastNameInitials = string.Concat(lastNameParts.Select(part => part[..1].ToLowerInvariant()));

            string baseUsername = $"{normalizedFirstName}{lastNameInitials}";

            int suffix = 0;
            string newUsername = baseUsername;

            while (existingUsernames.Contains(newUsername))
            {
                suffix++;
                newUsername = $"{baseUsername}{suffix}";
            }

            return newUsername;
        }

        private static string NormalizeAndRemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            string normalized = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
