using AssetManagement.Application.Common.Interfaces;

namespace AssetManagement.Application.Users.Queries.GetUsers;

public class UserBriefDto
{
    public string? Id { get; set; }
    public string? StaffCode { get; init; }

    public string? FullName { get; set; }

    public string? UserName { get; init; }

    public DateTime JoinDate { get; init; }

    public string? Type { get; set;} 

}