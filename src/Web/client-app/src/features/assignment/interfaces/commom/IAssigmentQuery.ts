export interface IAssignmentQuery {
  pageNumber: number;
  pageSize: number;
  sortColumnName: string;
  sortColumnDirection: string;
  state: string[];
  assignedDate: string;
  searchTerm: string;
}
