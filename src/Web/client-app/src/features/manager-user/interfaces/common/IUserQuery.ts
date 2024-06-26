export interface IUserQuery {
  pageNumber: number;
  pageSize: number;
  sortColumnName: string;
  sortColumnDirection: string;
  location: string;
  searchTerm: string;
  types: string[];
}
