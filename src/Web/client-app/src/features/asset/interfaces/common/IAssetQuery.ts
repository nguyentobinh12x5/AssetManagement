export interface IAssetQuery {
  pageNumber: number;
  pageSize: number;
  sortColumnName: string;
  sortColumnDirection: string;
  categoryName: string[];
  assetStatusName: string[];
  searchTerm: string;
}
