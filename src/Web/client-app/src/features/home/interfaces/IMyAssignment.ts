export interface IMyAssignmentBrief {
  id: number;
  assetCode: string;
  assetName: string;
  categoryName: string;
  assignedDate: string;
  state: number;
}
export interface IMyAssignmentQuery {
  pageNumber: number;
  pageSize: number;
  sortColumnName: string;
  sortColumnDirection: string;
}
