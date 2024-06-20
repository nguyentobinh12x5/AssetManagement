using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AssetManagement.Application.Users.Queries.GetUsers;
using AssetManagement.Application.Common.Models;
using AssetManagement.Domain.Constants;
using AssetManagement.Application.Common.Interfaces;

namespace AssetManagement.Application.Users.Queries.GetUsersByType;

public record GetUsersByTypeQuery : IRequest<PaginatedList<UserBriefDto>>
{
    public int PageNumber { get; init; } = AppPagingConstants.DefaultPageNumber;
    public int PageSize { get; init; } = AppPagingConstants.DefaultPageSize;
    public required string SortColumnName { get; init; }
    public required string SortColumnDirection { get; init; } = AppPagingConstants.DefaultSortDirection;
    public required string Type { get; init; } 
}

public class GetUsersByTypeQueryHandler : IRequestHandler<GetUsersByTypeQuery, PaginatedList<UserBriefDto>>
{
    private readonly IIdentityService _identityService;

    public GetUsersByTypeQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<PaginatedList<UserBriefDto>> Handle(GetUsersByTypeQuery request, CancellationToken cancellationToken)
    {
        var users = await _identityService.GetUsersByTypeAsync(request);

        return users;
    }


}



