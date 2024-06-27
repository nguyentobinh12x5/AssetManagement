export interface IAssetQuery {
  pageNumber: number;
  pageSize: number;
  sortColumnName: string;
  sortColumnDirection: string;
  category: string[];
  assetStatus: string[];
  searchTerm: string;
}
