using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Mappings;
using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Common.Security;
using AssetManagement.Domain.Constants;
using AssetManagement.Domain.Entities;
using AssetManagement.Domain.Enums;

namespace AssetManagement.Application.Assets.Queries.GetAssetsWithPagination;

[Authorize]
public class GetAssetsWithPaginationQuery : IRequest<PaginatedList<AssetBriefDto>>
{
    public int PageNumber { get; init; } = AppPagingConstants.DefaultPageNumber;
    public int PageSize { get; init; } = AppPagingConstants.DefaultPageSize;
    public required string SortColumnName { get; init; }
    public required string SortColumnDirection { get; init; } = AppPagingConstants.DefaultSortDirection;
    public string? SearchTerm { get; init; }

    public string[]? Category { get; set; } = [];

    public string[]? AssetStatus { get; set; } = [];
}

public class GetAssetsWithPaginationQueryHandler : IRequestHandler<GetAssetsWithPaginationQuery, PaginatedList<AssetBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUser _currentUser;

    public GetAssetsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper, IUser currentUser)
    {
        _context = context;
        _mapper = mapper;
        _currentUser = currentUser;
    }
    public async Task<PaginatedList<AssetBriefDto>> Handle(GetAssetsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Assets.AsQueryable();

        query = FilterAssets(request, _currentUser.Location, query);

        var assetDtosQuery = query
            .OrderByDynamic(request.SortColumnName, request.SortColumnDirection)
            .ProjectTo<AssetBriefDto>(_mapper.ConfigurationProvider)
            .Select(assetDto => new AssetBriefDto
            {
                Id = assetDto.Id,
                Code = assetDto.Code,
                Name = assetDto.Name,
                Category = assetDto.Category,
                AssetStatus = assetDto.AssetStatus,
                IsEnableAction = !_context.Assignments
                    .Where(a => a.Asset.Id == assetDto.Id)
                    .Any(a => a.State == AssignmentState.WaitingForAcceptance || a.State == AssignmentState.Accepted)
            });

        return await assetDtosQuery.PaginatedListAsync(request.PageNumber, request.PageSize);
    }

    private static IQueryable<Asset> FilterAssets(GetAssetsWithPaginationQuery request, string? adminLocation, IQueryable<Asset> query)
    {
        if (!string.IsNullOrEmpty(adminLocation))
        {
            query = query.Where(a => a.Location == adminLocation);
        }

        if (request.Category != null && request.Category.Any())
        {
            var categoryNames = request.Category
                .Select(c => c.Trim())
                .Where(c => !string.IsNullOrEmpty(c))
                .ToList();

            if (categoryNames.Any())
            {
                query = query.Where(a => categoryNames.Contains(a.Category.Name));
            }
        }

        if (request.AssetStatus != null && request.AssetStatus.Any())
        {
            var assetStatusNames = request.AssetStatus
                .Select(c => c.Trim())
                .Where(c => !string.IsNullOrEmpty(c))
                .ToList();

            if (assetStatusNames.Any())
            {
                query = query.Where(a => assetStatusNames.Contains(a.AssetStatus.Name));
            }
        }

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            query = query.Where(a => a.Name.Contains(request.SearchTerm) || a.Code.Contains(request.SearchTerm));
        }

        return query;
    }
}