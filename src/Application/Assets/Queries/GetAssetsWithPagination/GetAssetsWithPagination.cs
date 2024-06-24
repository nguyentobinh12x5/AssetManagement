using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Mappings;
using AssetManagement.Application.Common.Models;
using AssetManagement.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using AssetManagement.Domain.Constants;

namespace AssetManagement.Application.Assets.Queries.GetAssetsWithPagination;

public class GetAssetsWithPaginationQuery : IRequest<PaginatedList<AssetBriefDto>>
{
    public int PageNumber { get; init; } = AppPagingConstants.DefaultPageNumber;
    public int PageSize { get; init; } = AppPagingConstants.DefaultPageSize;
    public required string SortColumnName { get; init; }
    public required string SortColumnDirection { get; init; } = AppPagingConstants.DefaultSortDirection;
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
        return await _context.Assets
            //.OrderBy(x => x.Title)
            .OrderByDynamic(request.SortColumnName, request.SortColumnDirection)
            .ProjectTo<AssetBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}