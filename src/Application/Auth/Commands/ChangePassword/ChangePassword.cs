using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Security;

namespace AssetManagement.Application.Auth.Commands.ChangePassword;

[Authorize]
public record UpdatePasswordCommand : IRequest
{
    public string CurrentPassword { get; init; }
    public string NewPassword { get; init; }

    public UpdatePasswordCommand(string currentPassword, string newPassword)
    {
        CurrentPassword = currentPassword;
        NewPassword = newPassword;
    }
}

public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand>
{
    private readonly IIdentityService _identityService;

    public UpdatePasswordCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        var isValidPwd = await _identityService.CheckCurrentPassword(request.CurrentPassword);
        var result = await _identityService.ChangePasswordAsync(request.CurrentPassword, request.NewPassword);

        if (!result.Succeeded)
        {
            throw new ApplicationException("Failed to update password for the current user");
        }
    }
}