namespace AssetManagement.Domain.Entities;

public class Assignment : BaseAuditableEntity, ISoftDeletable
{

    public DateTime AssignedDate { get; set; } = DateTime.UtcNow;

    public AssignmentState State { get; set; } = AssignmentState.Accepted;

    public string Note { get; set; } = null!;

    public string AssignedTo { get; set; } = null!;

    public string AssignedBy { get; set; } = null!;

    public virtual Asset Asset { get; set; } = null!;
    public bool IsDelete { get; set; }
}