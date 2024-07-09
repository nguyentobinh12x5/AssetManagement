import { useEffect, useCallback } from 'react';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import useAppPaging from '../../../hooks/paging/useAppPaging';
import useAppSort from '../../../hooks/paging/useAppSort';
import { IReturningQuery } from '../interfaces/Common/IReturningQuery';
import { DEFAULT_MANAGE_RETURNING_SORT_COLUMN } from '../constants/returning-sort';
import { getReturnings, setReturningQuery } from '../reducers/returning-slice';

const useReturningList = () => {
  const dispatch = useAppDispatch();
  const { returnings, returningQuery, isDataFetched } = useAppState(
    (state) => state.returnings
  );

  const updateQuery = useCallback(
    (newQuery: Partial<IReturningQuery>) => {
      dispatch(setReturningQuery({ ...returningQuery, ...newQuery }));
    },
    [dispatch, returningQuery]
  );

  const updateMainSortState = useCallback(
    (sortColumnName: string, sortColumnDirection: string) => {
      updateQuery({ sortColumnName, sortColumnDirection });
    },
    [updateQuery]
  );

  const updateMainPagingState = useCallback(
    (page: number) => {
      updateQuery({ pageNumber: page });
    },
    [updateQuery]
  );

  const handleFilterByState = useCallback(
    (status: string[]) => {
      updateQuery({ pageNumber: 1, state: status });
    },
    [updateQuery]
  );

  const handleSearch = useCallback(
    (searchTerm: string) => {
      updateQuery({ pageNumber: 1, searchTerm: searchTerm.trim() });
    },
    [updateQuery]
  );

  const handleFilterByReturnedDate = useCallback(
    (returnedDate: string) => {
      updateQuery({ pageNumber: 1, returnedDate });
    },
    [updateQuery]
  );

  // Fetch Data
  useEffect(() => {
    if (!isDataFetched) {
      dispatch(getReturnings(returningQuery));
    }
  }, [dispatch, returningQuery, isDataFetched]);

  // Pagination and Sorting hooks
  const { hasSortColumn, handleSort } = useAppSort(
    DEFAULT_MANAGE_RETURNING_SORT_COLUMN,
    updateMainSortState
  );
  const { handlePaging } = useAppPaging(updateMainPagingState);

  return {
    hasSortColumn,
    returnings,
    searchTerm: returningQuery.searchTerm,
    handleSort,
    handlePaging,
    handleFilterByState,
    handleSearch,
    handleFilterByReturnedDate,
    sortColumnDirection: returningQuery.sortColumnDirection,
  };
};

export default useReturningList;
