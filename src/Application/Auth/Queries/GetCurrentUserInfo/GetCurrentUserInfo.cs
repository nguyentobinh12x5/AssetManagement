using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Security;

namespace AssetManagement.Application.Auth.Queries.GetCurrentUserInfo;

[Authorize]
public record GetCurrentUserInfoQuery : IRequest<UserInfoDto>;

public class GetCurrentUserInfoQueryHandler : IRequestHandler<GetCurrentUserInfoQuery, UserInfoDto>
{
    private readonly IIdentityService _identityService;
    private readonly IUser _currentUser;

    public GetCurrentUserInfoQueryHandler(IIdentityService identityService, IUser currentUser)
    {
        _identityService = identityService;
        _currentUser = currentUser;
    }
    public async Task<UserInfoDto> Handle(GetCurrentUserInfoQuery request, CancellationToken cancellationToken)
    {
        Guard.Against.NullOrWhiteSpace(_currentUser.Id);

        var user = await _identityService.GetCurrentUserInfo(_currentUser.Id);

        return user;
    }
}