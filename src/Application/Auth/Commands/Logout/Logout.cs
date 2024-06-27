using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Security;

namespace AssetManagement.Application.Auth.Commands.Logout;

[Authorize]
public record LogoutCommand() : IRequest;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
{
    private readonly IIdentityService _identityService;

    public LogoutCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        await _identityService.Logout();
    }
}