
using AssetManagement.Application.Common.Exceptions;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Security;

namespace AssetManagement.Application.Auth.Commands.ChangePasswordFirstTime;

[Authorize]
public record ChangePasswordFirstTimeCommand(string NewPassword) : IRequest;

public class ChangePasswordFirstTimeCommandHandler : IRequestHandler<ChangePasswordFirstTimeCommand>
{
    private readonly IIdentityService _identityService;

    public ChangePasswordFirstTimeCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    public async Task Handle(ChangePasswordFirstTimeCommand request, CancellationToken cancellationToken)
    {
        var isOldPassword = await _identityService.IsSameOldPassword(request.NewPassword);

        if (isOldPassword)
            throw new BadRequestException("New password must not the same as old one.");

        await _identityService.ChangePasswordFirstTimeAsync(request.NewPassword);
    }
}
