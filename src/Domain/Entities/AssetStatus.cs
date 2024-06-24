namespace AssetManagement.Domain.Entities;

public class AssetStatus : BaseEntity
{
    public string Name { get; set; } = null!;
    public virtual IList<Asset> Assets { get; set; } = new List<Asset>();
}