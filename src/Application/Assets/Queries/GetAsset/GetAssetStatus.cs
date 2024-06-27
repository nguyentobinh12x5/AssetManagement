using AssetManagement.Application.Common.Interfaces;

namespace AssetManagement.Application.Assets.Queries.GetAsset
{
    public record GetAssetStatus : IRequest<List<string>> { }

    public class GetAssetStatusHandler : IRequestHandler<GetAssetStatus, List<string>>
    {
        private readonly IApplicationDbContext _context;

        public GetAssetStatusHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> Handle(GetAssetStatus request, CancellationToken cancellationToken)
        {
            return await _context.AssetStatuses
                .Select(c => c.Name)
                .ToListAsync(cancellationToken);
        }
    }
}