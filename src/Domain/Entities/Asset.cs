namespace AssetManagement.Domain.Entities;

public class Asset : BaseAuditableEntity, ISoftDeletable
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string Specification { get; set; } = null!;
    public DateTime InstalledDate { get; set; } = DateTime.UtcNow;

    public virtual Category Category { get; set; } = null!;
    public virtual AssetStatus AssetStatus { get; set; } = null!;
    public bool IsDelete { get; set; }
}