using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Users.Queries.GetUsers;
using AssetManagement.Domain.Constants;

namespace AssetManagement.Application.Users.Queries.GetUsersBySearch;
public record GetUsersBySearchQuery : IRequest<PaginatedList<UserBriefDto>>
{
    public string? SearchTerm { get; init; }
    public int PageNumber { get; init; } = AppPagingConstants.DefaultPageNumber;
    public int PageSize { get; init; } = AppPagingConstants.DefaultPageSize;
    public string SortColumnName { get; init; } = "StaffCode";
    public string SortColumnDirection { get; init; } = AppPagingConstants.DefaultSortDirection;
}


public class GetUsersBySearchQueryHandler : IRequestHandler<GetUsersBySearchQuery, PaginatedList<UserBriefDto>>
{
    private readonly IIdentityService _identityService;

    public GetUsersBySearchQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<PaginatedList<UserBriefDto>> Handle(GetUsersBySearchQuery request, CancellationToken cancellationToken)
    {
        return await _identityService.GetUserBriefsBySearchAsync(request);
    }
}

