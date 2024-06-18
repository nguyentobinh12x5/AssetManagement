namespace AssetManagement.Application.Auth.Queries.GetCurrentUserInfo;


public sealed class UserInfoDto
{
    public UserInfoDto() { }

    public required string Email { get; init; }
    public required bool IsEmailConfirmed { get; init; }
    public IList<string> Roles { get; init; } = new List<string>();
}