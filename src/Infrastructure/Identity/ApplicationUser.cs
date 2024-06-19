using AssetManagement.Domain.Common;
using AssetManagement.Domain.Enums;

using Microsoft.AspNetCore.Identity;

namespace AssetManagement.Infrastructure.Identity;

public class ApplicationUser : IdentityUser, ISoftDeletable
{
    public string StaffCode { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Location { get; set; } = null!;
    public DateTime DateOfBirth { get; set; } = DateTime.UtcNow;
    public Gender Gender { get; set; } = Gender.Male;
    public DateTime JoinDate { get; set; }
    public bool MustChangePassword { get; set; } = true;
    public bool IsDelete { get; set; }

}