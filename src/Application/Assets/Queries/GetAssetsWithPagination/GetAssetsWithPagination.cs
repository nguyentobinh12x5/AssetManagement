using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Mappings;
using AssetManagement.Application.Common.Models;
using AssetManagement.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using AssetManagement.Domain.Constants;
using AssetManagement.Domain.Entities;

namespace AssetManagement.Application.Assets.Queries.GetAssetsWithPagination;

public class GetAssetsWithPaginationQuery : IRequest<PaginatedList<AssetBriefDto>>
{
    public int PageNumber { get; init; } = AppPagingConstants.DefaultPageNumber;
    public int PageSize { get; init; } = AppPagingConstants.DefaultPageSize;
    public required string SortColumnName { get; init; }
    public required string SortColumnDirection { get; init; } = AppPagingConstants.DefaultSortDirection;
    
    public string? CategoryName { get; init; }
    
    public string? AssetStatusName { get; init; }
    
    public string? SearchTerm { get; init; }
}

public class GetAssetsWithPaginationQueryHandler : IRequestHandler<GetAssetsWithPaginationQuery, PaginatedList<AssetBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    
    public GetAssetsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PaginatedList<AssetBriefDto>> Handle(GetAssetsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Assets.AsQueryable();
        
        query = FilterAssets(request, query);

        return await query
            //.OrderBy(x => x.Title)
            .OrderByDynamic(request.SortColumnName, request.SortColumnDirection)
            .ProjectTo<AssetBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }

    private static IQueryable<Asset> FilterAssets(GetAssetsWithPaginationQuery request, IQueryable<Asset> query)
    {
        if (!string.IsNullOrEmpty(request.CategoryName))
        {
            query = query.Where(a => a.Category.Name == request.CategoryName);
        }

        if (!string.IsNullOrEmpty(request.AssetStatusName))
        {
            query = query.Where(a => a.AssetStatus.Name == request.AssetStatusName);
        }
        
        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            query = query.Where(a => a.Name.Contains(request.SearchTerm) || a.Code.Contains(request.SearchTerm));
        }

        return query;
    }
}