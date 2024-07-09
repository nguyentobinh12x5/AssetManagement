using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Domain.Enums;

using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Application.Assignments.Queries.GetAssignment
{
    public record GetAssignmentsByAssetIdQuery(int AssetId) : IRequest<List<AssignmentDto>>;

    public class GetAssignmentsByAssetIdQueryHandler : IRequestHandler<GetAssignmentsByAssetIdQuery, List<AssignmentDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAssignmentsByAssetIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AssignmentDto>> Handle(GetAssignmentsByAssetIdQuery request, CancellationToken cancellationToken)
        {
            var assignments = await _context.Assignments
                .Where(a => a.Asset.Id == request.AssetId && (a.State == AssignmentState.WaitingForAcceptance || a.State == AssignmentState.Accepted))
                .Include(a => a.Asset)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<AssignmentDto>>(assignments);
        }
    }
}