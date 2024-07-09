
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Mappings;
using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Common.Security;
using AssetManagement.Domain.Constants;
using AssetManagement.Domain.Entities;
using AssetManagement.Domain.Enums;

namespace AssetManagement.Application.ReturningRequests.Queries.GetReturningRequestsWithPagination;

[Authorize]
public class GetReturningRequestsWithPaginationQuery : IRequest<PaginatedList<ReturningRequestBriefDto>>
{
    public int PageNumber { get; init; } = AppPagingConstants.DefaultPageNumber;
    public int PageSize { get; init; } = AppPagingConstants.DefaultPageSize;
    public required string SortColumnName { get; init; }
    public required string SortColumnDirection { get; init; } = AppPagingConstants.DefaultSortDirection;
    public string? SearchTerm { get; init; }
    public string[]? State { get; init; } = [];
    public string? ReturnedDate { get; init; }
}

public class GetReturningRequestsWithPaginationQueryHandler : IRequestHandler<GetReturningRequestsWithPaginationQuery, PaginatedList<ReturningRequestBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetReturningRequestsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PaginatedList<ReturningRequestBriefDto>> Handle(
        GetReturningRequestsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.ReturningRequests.AsQueryable();

        query = FilterReturningRequests(request, query);

        var result = await query
                        .OrderByDynamic(request.SortColumnName, request.SortColumnDirection)
                        .ProjectTo<ReturningRequestBriefDto>(_mapper.ConfigurationProvider)
                        .PaginatedListAsync(request.PageNumber, request.PageSize);
        return result;
    }

    private static IQueryable<ReturningRequest> FilterReturningRequests(
        GetReturningRequestsWithPaginationQuery request,
        IQueryable<ReturningRequest> query)
    {
        if (request.State != null && request.State.Any())
        {
            var states = request.State
                            .Select(s => s.Trim())
                            .Where(s => !string.IsNullOrWhiteSpace(s))
                            .ToList();

            if (states.Any())
            {
                var enumStates = states
                                    .Select(s => Enum.TryParse<ReturningRequestState>(s, true, out var state) ? state : (ReturningRequestState?)null)
                                    .Where(s => s.HasValue)
                                    .Select(s => s!.Value)
                                    .ToList();

                if (enumStates.Any())
                {
                    query = query.Where(q => enumStates.Contains(q.State));
                }
            }
        }

        if (!string.IsNullOrEmpty(request.ReturnedDate))
        {
            if (DateTime.TryParse(request.ReturnedDate, out var ReturnedDate))
            {
                query = query.Where(a => a.ReturnedDate!.Value.Date == ReturnedDate.Date);
            }
        }

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            query = query.Where(q =>
                            q.Assignment.Asset.Code.Contains(request.SearchTerm)
                            || q.Assignment.Asset.Name.Contains(request.SearchTerm)
                            || q.RequestedBy.Contains(request.SearchTerm));
        }

        return query;
    }
}