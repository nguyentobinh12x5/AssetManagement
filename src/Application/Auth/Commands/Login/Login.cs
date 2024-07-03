
using AssetManagement.Application.Common.Interfaces;

namespace AssetManagement.Application.Auth.Commands.Login;

public record LoginCommand(
    string Email,
    string Password,
    string? TwoFactorCode,
    string? TwoFactorRecoveryCode,
    bool? UseCookies,
    bool? UseSessionCookies) : IRequest;

public class LoginCommandHandler : IRequestHandler<LoginCommand>
{
    private readonly IIdentityService _identityService;

    public LoginCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        await _identityService.Login(request);
    }
}