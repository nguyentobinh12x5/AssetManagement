using AssetManagement.Application.Assignments.Commands.Create;
using AssetManagement.Application.Assignments.Commands.Update;
using AssetManagement.Application.Assignments.Queries.GetAssignment;
using AssetManagement.Application.Assignments.Queries.GetAssignmentsWithPagination;
using AssetManagement.Application.Assignments.Queries.GetMyAssignments;
using AssetManagement.Application.Common.Models;

namespace AssetManagement.Web.Endpoints;

public class Assignments : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .AllowAnonymous()
            .MapGet(GetAssignmentList)
            .MapGet(GetMyAssignments, "me")
            .MapPost(AddAssignment)
            .MapGet(GetAssignmentById, "{id}")
            .MapPatch(UpdateMyAssignmentState, "{id}");
    }

    public Task<PaginatedList<AssignmentBriefDto>> GetAssignmentList(ISender sender, [AsParameters] GetAssignmentsWithPaginationQuery query)
    {
        return sender.Send(query);
    }

    public Task<int> AddAssignment(ISender sender, CreateNewAssignmentCommand command)
    {
        return sender.Send(command);
    }

    public async Task<PaginatedList<MyAssignmentDto>> GetMyAssignments(ISender sender, [AsParameters] GetMyAssignmentsQuery query)
    {
        return await sender.Send(query);
    }
    public async Task<AssignmentDto> GetAssignmentById(ISender sender, int id)
    {
        return await sender.Send(new GetAssignmentByIdQuery(id));
    }

    public async Task<IResult> UpdateMyAssignmentState(ISender sender, int id, UpdateMyAssignmentStateCommand command)
    {
        if (id != command.Id)
        {
            return Results.BadRequest();
        }
        await sender.Send(command);
        return Results.NoContent();
    }
}