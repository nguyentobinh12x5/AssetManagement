using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Security;

namespace AssetManagement.Application.Assets.Commands.Delete;

[Authorize]
public record DeleteAssetCommand(int id): IRequest;

[Authorize]
public class DeleteAssetCommandHandler : IRequestHandler<DeleteAssetCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteAssetCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
    {
        var asset = await _context.Assets.FindAsync(request.id);

        Guard.Against.NotFound(request.id, asset);

        _context.Assets.Remove(asset);
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}

