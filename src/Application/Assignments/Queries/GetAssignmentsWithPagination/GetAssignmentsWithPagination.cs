using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Mappings;
using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Common.Security;
using AssetManagement.Domain.Constants;
using AssetManagement.Domain.Entities;
using AssetManagement.Domain.Enums;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR;

namespace AssetManagement.Application.Assignments.Queries.GetAssignmentsWithPagination;

[Authorize]
public class GetAssignmentsWithPaginationQuery : IRequest<PaginatedList<AssignmentBriefDto>>
{
    public int PageNumber { get; init; } = AppPagingConstants.DefaultPageNumber;
    public int PageSize { get; init; } = AppPagingConstants.DefaultPageSize;
    public required string SortColumnName { get; init; }
    public required string SortColumnDirection { get; init; } = AppPagingConstants.DefaultSortDirection;
    public string[]? State { get; init; } = [];
    public string? AssignedDate { get; init; }
    public string? SearchTerm { get; init; }
}

public class GetAssignmentsWithPaginationQueryHandler : IRequestHandler<GetAssignmentsWithPaginationQuery, PaginatedList<AssignmentBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAssignmentsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<AssignmentBriefDto>> Handle(GetAssignmentsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Assignments.AsQueryable();

        query = FilterAssignments(request, query);

        return await query
            .OrderByDynamic(request.SortColumnName, request.SortColumnDirection)
            .ProjectTo<AssignmentBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }

    private static IQueryable<Assignment> FilterAssignments(GetAssignmentsWithPaginationQuery request, IQueryable<Assignment> query)
    {
        if (request.State != null && request.State.Any())
        {
            var stateNames = request.State
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrEmpty(s))
                .ToList();

            if (stateNames.Any())
            {
                var states = stateNames
                    .Select(s => Enum.TryParse<AssignmentState>(s, true, out var state) ? state : (AssignmentState?)null)
                    .Where(s => s.HasValue)
                    .Select(s => s!.Value)
                    .ToList();

                if (states.Any())
                {
                    query = query.Where(a => states.Contains(a.State));
                }
            }
        }

        if (!string.IsNullOrEmpty(request.AssignedDate))
        {
            if (DateTime.TryParse(request.AssignedDate, out var assignedDateTime))
            {
                query = query.Where(a => a.AssignedDate.Date == assignedDateTime.Date);
            }
        }

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            query = query.Where(a => a.Asset.Name.Contains(request.SearchTerm)
                                     || a.Asset.Code.Contains(request.SearchTerm)
                                     || a.AssignedTo.Contains(request.SearchTerm)
                                     || a.AssignedBy.Contains(request.SearchTerm));
        }

        return query;
    }
}