namespace AssetManagement.Domain.Entities;

public class ReturningRequest : BaseAuditableEntity, ISoftDeletable
{
    public string RequestedBy { get; set; } = null!;
    public string? AcceptedBy { get; set; }
    public DateTime? ReturnedDate { get; set; }
    public ReturningRequestState State { get; set; } = ReturningRequestState.WaitingForReturning;
    public bool IsDelete { get; set; }

    public int AssignmentId { get; set; }

    public virtual Assignment Assignment { get; set; } = null!;
}