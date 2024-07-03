using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Security;
using AssetManagement.Domain.Entities;


namespace AssetManagement.Application.Assets.Commands.Update;
[Authorize]
public record UpdateAssetCommand : IRequest<int>
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string Specification { get; init; } = null!;
    public DateTime InstalledDate { get; init; }
    public string State { get; init; } = null!;
}
[Authorize]
public class UpdateAssetCommandHandler : IRequestHandler<UpdateAssetCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _currentUser;

    public UpdateAssetCommandHandler(IApplicationDbContext context, IUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<int> Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
    {
        var asset = await _context.Assets
            .Include(a => a.AssetStatus)
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        Guard.Against.NotFound(nameof(Asset), asset);

        var state = await _context.AssetStatuses.FirstOrDefaultAsync(e => e.Name == request.State, cancellationToken);

        Guard.Against.NotFound(nameof(AssetStatus), state);

        asset.Name = request.Name;
        asset.Specification = request.Specification;
        asset.InstalledDate = request.InstalledDate;
        asset.AssetStatus = state;

        await _context.SaveChangesAsync(cancellationToken);

        return asset.Id;
    }
}