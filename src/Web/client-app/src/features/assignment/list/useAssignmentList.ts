import { useEffect } from 'react';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import useAppPaging from '../../../hooks/paging/useAppPaging';
import useAppSort from '../../../hooks/paging/useAppSort';
import { DEFAULT_MANAGE_ASSIGNMENT_SORT_COLUMN } from '../constants/assignment-sort';
import {
  getAssignments,
  setAssignmentQuery,
} from '../reducers/assignment-slice';

const useAssignmentList = () => {
  const dispatch = useAppDispatch();
  const { assignments, assignmentQuery, isDataFetched } = useAppState(
    (state) => state.assignments
  );

  const updateMainSortState = (
    sortColumnName: string,
    sortColumnDirection: string
  ) => {
    dispatch(
      setAssignmentQuery({
        ...assignmentQuery,
        sortColumnName,
        sortColumnDirection,
      })
    );
  };

  const updateMainPagingState = (page: number) => {
    dispatch(
      setAssignmentQuery({
        ...assignmentQuery,
        pageNumber: page,
      })
    );
  };

  const { hasSortColumn, handleSort } = useAppSort(
    DEFAULT_MANAGE_ASSIGNMENT_SORT_COLUMN,
    updateMainSortState
  );
  const { handlePaging } = useAppPaging(updateMainPagingState);

  const handleFilterByState = (status: string[]) => {
    dispatch(
      setAssignmentQuery({
        ...assignmentQuery,
        pageNumber: 1,
        state: status,
      })
    );
  };

  const handleSearch = (searchTerm: string) => {
    dispatch(
      setAssignmentQuery({
        ...assignmentQuery,
        pageNumber: 1,
        searchTerm: searchTerm.trim(),
      })
    );
  };

  const handleFilterByAssignedDate = (assignedDate: string) => {
    dispatch(
      setAssignmentQuery({
        ...assignmentQuery,
        pageNumber: 1,
        assignedDate,
      })
    );
  };
  // Fetch Data
  useEffect(() => {
    if (!isDataFetched) {
      dispatch(getAssignments(assignmentQuery));
    }
  }, [dispatch, assignmentQuery, isDataFetched]);

  return {
    hasSortColumn,
    assignments,
    searchTerm: assignmentQuery.searchTerm,
    handleSort,
    handlePaging,
    handleFilterByState,
    handleSearch,
    handleFilterByAssignedDate,
  };
};

export default useAssignmentList;
