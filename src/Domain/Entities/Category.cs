namespace AssetManagement.Domain.Entities;

public class Category : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public virtual IList<Asset> Assets { get; private set; } = new List<Asset>();
}