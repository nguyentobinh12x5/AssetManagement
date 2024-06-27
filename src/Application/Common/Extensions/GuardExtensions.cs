using AssetManagement.Application.Common.Models;

namespace AssetManagement.Application.Common.Extensions
{
    public static class GuardExtensions
    {
        public static void EnsureSucceedResult<T>(this IGuardClause guard, T entity, string message)
        {
            if (entity == null)
            {
                throw new ArgumentException(message);
            }

            if (entity is Result result && !result.Succeeded)
            {
                throw new ArgumentException(message);
            }
        }
    }
}