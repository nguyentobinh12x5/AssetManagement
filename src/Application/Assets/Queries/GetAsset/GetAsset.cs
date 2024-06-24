using System.Diagnostics.Metrics;

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Domain.Entities;

namespace AssetManagement.Application.Assets.Queries.GetDetailedAssets;

public record GetAssetByIdQuery(int Id) : IRequest<AssetDto>;


public class GetAssetByIdQueryHandler : IRequestHandler<GetAssetByIdQuery, AssetDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAssetByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AssetDto> Handle(GetAssetByIdQuery request, CancellationToken cancellationToken)
    {
        var asset = await _context.Assets
            .Include(a => a.Category)
            .Include(a => a.AssetStatus)
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, asset);

        return _mapper.Map<AssetDto>(asset);
    }
}
