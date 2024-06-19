using AssetManagement.Domain.Enums;

namespace AssetManagement.Application.Users.Queries.GetUser;

public class UserDto
{
    public string Id { get; set; } = null!;

    public string StaffCode { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public DateTime JoinDate { get; set; }

    public Gender Gender { get; set; }

    public string Type { get; set; } = null!;

    public string Location { get; set; } = null!;
}