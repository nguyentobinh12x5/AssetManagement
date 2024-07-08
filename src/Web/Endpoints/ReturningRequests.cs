namespace AssetManagement.Web.Endpoints;

public class ReturningRequests : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .AllowAnonymous();
    }
}