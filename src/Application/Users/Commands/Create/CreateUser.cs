using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Domain.Enums;

namespace AssetManagement.Application.Users.Commands.Create;
public record CreateUserCommand : IRequest<string>
{
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Location { get; init; } = null!;
    public DateTime DateOfBirth { get; init; } = DateTime.UtcNow;
    public Gender Gender { get; init; } = Gender.Male;
    public DateTime JoinDate { get; init; }
    public string Type { get; init; } = null!;
}
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;
    public CreateUserCommandHandler(IApplicationDbContext context, IIdentityService identityService)
    {
        _context = context;
        _identityService = identityService;
    }
    public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new CreateUserDto
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Location = request.Location,
            Gender = request.Gender,
            JoinDate = request.JoinDate,
            DateOfBirth = request.DateOfBirth,
            Role = request.Type,
        };
        var newUser = _identityService.CreateUserAsync(user);

        await _context.SaveChangesAsync(cancellationToken);

        return newUser.Result.Id;
    }
}