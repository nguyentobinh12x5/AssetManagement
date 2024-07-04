import SORT_TYPE from '../components/table/constants/SortType';
import { ASCENDING, DESCENDING } from '../constants/paging';
import ISort from '../interfaces/ISort';

const getSortOrder = (hasQuery: ISort, sortColumn: string): string => {
  let sortOrder;
  if (hasQuery.sortColumn !== sortColumn) {
    sortOrder = ASCENDING;
  } else {
    sortOrder = hasQuery.sortOrder === ASCENDING ? DESCENDING : ASCENDING;
  }
  return sortOrder;
};

const paramsSerializer = (params: any) => {
  const searchParams = new URLSearchParams();
  Object.keys(params).forEach((key) => {
    const value = params[key];
    if (Array.isArray(value)) {
      value.forEach((val) => searchParams.append(key, val));
    } else {
      searchParams.append(key, value);
    }
  });
  return searchParams.toString();
};

export interface PaginationInfo {
  pageNumber: number;
  pageSize: number;
  totalCount: number; // or totalCount, depending on your setup
  sortDirection: string;
}

function calculateNo(
  index: number,
  { pageNumber, pageSize, totalCount, sortDirection }: PaginationInfo
): number {
  if (sortDirection === SORT_TYPE.ASCENDING) {
    return (pageNumber - 1) * pageSize + index + 1;
  } else {
    return totalCount - ((pageNumber - 1) * pageSize + index);
  }
}

const serilizeArrayQuery = (values: any[], key: string) => {
  return values.reduce((accum, curr) => accum + `&${key}=${curr}`, '');
};

export { getSortOrder, paramsSerializer, serilizeArrayQuery, calculateNo };
