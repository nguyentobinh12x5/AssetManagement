using AssetManagement.Application.Common.Interfaces;

namespace AssetManagement.Application.Assets.Queries.GetAsset
{
    public record GetAssetCategories : IRequest<List<string>> { }

    public class GetAssetCategoryHandler : IRequestHandler<GetAssetCategories, List<string>>
    {
        private readonly IApplicationDbContext _context;

        public GetAssetCategoryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> Handle(GetAssetCategories request, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .Select(c => c.Name)
                .ToListAsync(cancellationToken);
        }
    }
}