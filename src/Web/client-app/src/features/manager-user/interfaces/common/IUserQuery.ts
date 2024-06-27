export interface IUserQuery {
  pageNumber: number;
  pageSize: number;
  sortColumnName: string;
  sortColumnDirection: string;
  searchTerm: string;
  types: string[];
}
