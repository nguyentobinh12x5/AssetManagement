﻿using AssetManagement.Application.Common.Interfaces;

namespace AssetManagement.Application.Users.Queries.GetUser;

public record GetUserQuery() : IRequest<UserDto>
{
    public string Id { get; set; } = null!;
};

public class GetUsersQueryHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly IIdentityService _identityService;

    public GetUsersQueryHandler(
        IIdentityService identityService
    )
    {
        _identityService = identityService;
    }

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _identityService.GetUserWithRoleAsync(request.Id);

        return user;
    }
}