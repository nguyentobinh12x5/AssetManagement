namespace AssetManagement.Application.Common.Interfaces;

public interface IUser
{
    string? Id { get; }
    string? UserName { get; }
    string? Location { get; }
}