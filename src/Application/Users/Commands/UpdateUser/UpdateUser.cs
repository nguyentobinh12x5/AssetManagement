using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Domain.Enums;

namespace AssetManagement.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand : IRequest
{
    public string Id { get; init; } = null!;

    public DateTime DateOfBirth { get; init; }

    public DateTime JoinDate { get; init; }

    public Gender Gender { get; init; }

    public string Type { get; init; } = null!;

}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IIdentityService _identityService;

    public UpdateUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await _identityService.GetUserWithRoleAsync(request.Id);
        
        currentUser.DateOfBirth = request.DateOfBirth;
        currentUser.Gender = request.Gender;
        currentUser.JoinDate = request.JoinDate;

        await _identityService.UpdateUserAsync(currentUser);

        if (currentUser.Type != request.Type)
        {
            await _identityService.UpdateUserToRoleAsync(request.Id, currentUser.Type, request.Type);
        }
    }
}