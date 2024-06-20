using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Mappings;
using AssetManagement.Application.Common.Models;
using AssetManagement.Domain.Constants;

namespace AssetManagement.Application.TodoItems.Queries.GetTodoItemsWithPagination;

public record GetTodoItemsWithPaginationQuery : IRequest<PaginatedList<TodoItemBriefDto>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = AppPagingConstants.DefaultPageNumber;
    public int PageSize { get; init; } = AppPagingConstants.DefaultPageSize;
    public required string SortColumnName { get; init; }
    public required string SortColumnDirection { get; init; } = AppPagingConstants.DefaultSortDirection;
}

public class GetTodoItemsWithPaginationQueryHandler : IRequestHandler<GetTodoItemsWithPaginationQuery, PaginatedList<TodoItemBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTodoItemsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<TodoItemBriefDto>> Handle(GetTodoItemsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.TodoItems
            .Where(x => x.ListId == request.ListId)
            //.OrderBy(x => x.Title)
            .OrderByDynamic(request.SortColumnName, request.SortColumnDirection)
            .ProjectTo<TodoItemBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}