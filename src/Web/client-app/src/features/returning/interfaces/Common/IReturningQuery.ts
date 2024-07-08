export interface IReturningQuery {
    pageNumber: number;
    pageSize: number;
    sortColumnName: string;
    sortColumnDirection: string;
    state: string[];
    returnedDate: string;
    searchTerm: string;
}