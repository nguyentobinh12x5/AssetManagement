using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Common.Security;
using AssetManagement.Domain.Constants;

namespace AssetManagement.Application.Users.Queries.GetUsers;

[Authorize]
public record GetUsersQuery : IRequest<PaginatedList<UserBriefDto>>
{
    public int PageNumber { get; init; } = AppPagingConstants.DefaultPageNumber;
    public int PageSize { get; init; } = AppPagingConstants.DefaultPageSize;
    public required string SortColumnName { get; init; } = "StaffCode";
    public required string SortColumnDirection { get; init; } = AppPagingConstants.DefaultSortDirection;
    public string? SearchTerm { get; init; }
    public string Types { get; init; } = String.Empty;
}

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PaginatedList<UserBriefDto>>
{
    private readonly IIdentityService _identityService;

    public GetUsersQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<PaginatedList<UserBriefDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _identityService.GetUserBriefsAsync(request);

        return users;
    }

}