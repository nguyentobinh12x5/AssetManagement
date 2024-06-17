using AssetManagement.Application.Users.Commands.DeleteUser;

namespace AssetManagement.Web.Endpoints
{
	public class Users : EndpointGroupBase
	{
		public override void Map(WebApplication app)
		{
			app.MapGroup(this)
				.AllowAnonymous()
				.MapDelete(DeleteUser,"{id}");
		}
		public async Task<IResult> DeleteUser(ISender sender, string id)
		{
			await sender.Send(new DeleteUserCommand(id));
			return Results.NoContent();
		}
	}
}
