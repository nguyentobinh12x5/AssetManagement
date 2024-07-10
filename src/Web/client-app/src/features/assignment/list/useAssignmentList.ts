import { useCallback } from 'react';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import useAppPaging from '../../../hooks/paging/useAppPaging';
import useAppSort from '../../../hooks/paging/useAppSort';
import { DEFAULT_MANAGE_ASSIGNMENT_SORT_COLUMN } from '../constants/assignment-sort';
import { setAssignmentQuery } from '../reducers/assignment-slice';
import { IAssignmentQuery } from '../interfaces/commom/IAssigmentQuery';

const useAssignmentList = () => {
  const dispatch = useAppDispatch();
  const { assignments, assignmentQuery } = useAppState(
    (state) => state.assignments
  );

  const updateQuery = useCallback(
    (newQuery: Partial<IAssignmentQuery>) => {
      dispatch(setAssignmentQuery({ ...assignmentQuery, ...newQuery }));
    },
    [dispatch, assignmentQuery]
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

  const handleFilterByAssignedDate = useCallback(
    (assignedDate: string) => {
      updateQuery({ pageNumber: 1, assignedDate });
    },
    [updateQuery]
  );

  // Pagination and Sorting hooks
  const { hasSortColumn, handleSort } = useAppSort(
    DEFAULT_MANAGE_ASSIGNMENT_SORT_COLUMN,
    updateMainSortState
  );
  const { handlePaging } = useAppPaging(updateMainPagingState);

  return {
    hasSortColumn,
    handleSort,
    handlePaging,
    handleFilterByState,
    handleSearch,
    handleFilterByAssignedDate,
    sortColumnDirection: assignmentQuery.sortColumnDirection,
  };
};

export default useAssignmentList;
