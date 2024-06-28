using AssetManagement.Application.Common.Extensions;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Security;
using AssetManagement.Domain.Entities;

namespace AssetManagement.Application.Assets.Commands.Create;
[Authorize]
public record CreateNewAssetCommand : IRequest<int>
{
    public string Name { get; init; } = null!;
    public string Category { get; init; } = null!;
    public string Specification { get; init; } = null!;
    public DateTime InstallDate { get; init; } = DateTime.UtcNow;
    public string State { get; init; } = null!;
}
[Authorize]
public class CreateNewAssetCommandHandler : IRequestHandler<CreateNewAssetCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _currentUser;
    public CreateNewAssetCommandHandler(IApplicationDbContext context, IUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<int> Handle(CreateNewAssetCommand request, CancellationToken cancellationToken)
    {
        var CodeList = await _context.Assets.Select(c => c.Code).ToListAsync();

        var category = await _context.Categories.FirstOrDefaultAsync(e => e.Name == request.Category);

        Guard.Against.NotFound(request.Category, category);

        var state = await _context.AssetStatuses.FirstOrDefaultAsync(e => e.Name == request.State);

        Guard.Against.NotFound(request.State, state);

        var newAsset = new Asset
        {
            Code = CodeList.GenerateNewAssetCode(category.Code),
            Location = _currentUser.Location!,
            Name = request.Name,
            Specification = request.Specification,
            InstalledDate = request.InstallDate,
            AssetStatus = state,
            Category = category,
        };

        _context.Assets.Add(newAsset);

        await _context.SaveChangesAsync(cancellationToken);

        return newAsset.Id;

    }
}