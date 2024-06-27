using AssetManagement.Application.Common.Interfaces;

namespace AssetManagement.Application.Users.Queries.GetUsers;

public record GetUserTypes : IRequest<List<string>> { }

public class GetUserTypesHandler : IRequestHandler<GetUserTypes, List<string>>
{
    private readonly IIdentityService _identityService;

    public GetUserTypesHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<List<string>> Handle(GetUserTypes request, CancellationToken cancellationToken)
    {
        var users = await _identityService.GetUserTypes();

        return users.Where(user => user != null).Cast<string>().ToList();
    }

}