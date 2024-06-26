using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Application.Common.Extensions
{
    public static class AssetCodeGenerator
    {
        public static string GenerateNewAssetCode(this IEnumerable<string> existingCodes, string Category)
        {
            string prefix = Category;
            int maxNumber = existingCodes
                .Where(code => code.StartsWith(prefix))
                .Select(code => int.Parse(code.Substring(prefix.Length)))
                .DefaultIfEmpty(0)
                .Max();
            return $"{prefix}{(maxNumber + 1).ToString("D6")}";
        }
    }
}