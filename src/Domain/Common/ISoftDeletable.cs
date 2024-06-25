namespace AssetManagement.Domain.Common;

public interface ISoftDeletable
{
    public bool IsDelete { get; set; }
    public void Undo()
    {
        IsDelete = false;
    }

}