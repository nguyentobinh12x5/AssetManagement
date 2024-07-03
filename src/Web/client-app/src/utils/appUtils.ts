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

const serilizeArrayQuery = (values: any[], key: string) => {
  return values.reduce((accum, curr) => accum + `&${key}=${curr}`, '');
};

export { getSortOrder, paramsSerializer, serilizeArrayQuery };
