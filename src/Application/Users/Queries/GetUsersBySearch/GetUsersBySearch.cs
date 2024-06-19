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
public record GetUsersBySearchQuery : IRequest<List<UserBriefDto>>
{
    public string? SearchTerm { get; init; }
    //public string? FullName { get; init; }
    //public string? StaffCode { get; init; }
}
public class GetUsersBySearchQueryHandler : IRequestHandler<GetUsersBySearchQuery, List<UserBriefDto>>
{
    private readonly IIdentityService _identityService;

    public GetUsersBySearchQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<List<UserBriefDto>> Handle(GetUsersBySearchQuery request, CancellationToken cancellationToken)
    {
        //var users = await _identityService.GetUserBriefsBySearchAsync(request);

        //return users;

        return await _identityService.GetUserBriefsBySearchAsync(request);
    }
}
