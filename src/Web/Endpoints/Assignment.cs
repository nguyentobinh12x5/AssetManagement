namespace AssetManagement.Web.Endpoints;

public class Assignment : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .AllowAnonymous();
    }

}