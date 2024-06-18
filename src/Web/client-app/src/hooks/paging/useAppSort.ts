import { useState } from 'react';
import { ASCENDING } from '../../constants/paging';
import ISort from '../../interfaces/ISort';
import { getSortOrder } from '../../utils/appUtils';

const useAppSort = (
  sortColumn: string,
  updateMainSortState?: (
    sortColumnName: string,
    sortColumnDirection: string
  ) => void
) => {
  const defaultSort: ISort = {
    sortColumn: sortColumn,
    sortOrder: ASCENDING,
  };

  const [hasSortColumn, setSortColumn] = useState(defaultSort);

  const handleSort = (sortColumn: string) => {
    let sortColumnDirection = getSortOrder(hasSortColumn, sortColumn);
    setSortColumn({
      ...hasSortColumn,
      sortColumn,
      sortOrder: sortColumnDirection,
    });

    //update state back to main
    if (updateMainSortState)
      updateMainSortState(sortColumn, sortColumnDirection);
  };

  return {
    hasSortColumn,
    handleSort,
  };
};

export default useAppSort;
