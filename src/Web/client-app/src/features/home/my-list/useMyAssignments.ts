import { useEffect } from 'react';
import useAppPaging from '../../../hooks/paging/useAppPaging';
import useAppSort from '../../../hooks/paging/useAppSort';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import { DEFAULT_MY_ASSIGNMENT_SORT_COL } from '../constants/my-assignment-query';
import {
  getMyAssignments,
  setMyAssignmentQuery,
} from '../reducers/my-assignment-slice';

const useMyAssignments = () => {
  const dispatch = useAppDispatch();
  const { query, assignments } = useAppState((state) => state.myAssignments);

  const updateMainSortState = (
    sortColumnName: string,
    sortColumnDirection: string
  ) => {
    dispatch(
      setMyAssignmentQuery({
        ...query,
        sortColumnName,
        sortColumnDirection,
      })
    );
  };

  const updateMainPagingState = (page: number) => {
    dispatch(
      setMyAssignmentQuery({
        ...query,
        pageNumber: page,
      })
    );
  };

  const { hasSortColumn, handleSort } = useAppSort(
    DEFAULT_MY_ASSIGNMENT_SORT_COL,
    updateMainSortState
  );
  const { handlePaging } = useAppPaging(updateMainPagingState);

  // Fetch Data
  useEffect(() => {
    dispatch(getMyAssignments(query));
  }, [dispatch, query]);

  return {
    assignments,
    hasSortColumn,
    handleSort,
    handlePaging,
  };
};

export default useMyAssignments;
