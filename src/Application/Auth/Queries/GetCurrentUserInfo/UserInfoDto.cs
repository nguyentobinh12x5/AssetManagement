namespace AssetManagement.Application.Auth.Queries.GetCurrentUserInfo;


public sealed class UserInfoDto
{
    public UserInfoDto() { }

    public required string Username { get; init; }
    public required bool MustChangePassword { get; init; }
    public IList<string> Roles { get; init; } = new List<string>();
}