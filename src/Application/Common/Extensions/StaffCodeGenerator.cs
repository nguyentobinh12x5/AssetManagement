namespace AssetManagement.Application.Common.Extensions
{
    public static class StaffCodeGenerator
    {
        public static string GenerateNewStaffCode(this IEnumerable<string> existingCodes)
        {
            const string prefix = "SD";
            int maxNumber = existingCodes
                .Where(code => code.StartsWith(prefix))
                .Select(code => int.Parse(code.Substring(prefix.Length)))
                .DefaultIfEmpty(0)
                .Max();
            return $"{prefix}{(maxNumber + 1).ToString("D4")}";
        }
    }
}