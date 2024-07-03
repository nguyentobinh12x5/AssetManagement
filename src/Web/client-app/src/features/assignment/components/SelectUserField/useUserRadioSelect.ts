import { useEffect } from 'react';
import useAppPaging from '../../../../hooks/paging/useAppPaging';
import useAppSort from '../../../../hooks/paging/useAppSort';
import { useAppDispatch, useAppState } from '../../../../redux/redux-hooks';
import { DEFAULT_MANAGE_USER_SORT_COLUMN } from '../../../manager-user/constants/user-sort';
import {
  getUsers,
  resetUserSlice,
  setUserQuery,
} from '../../../manager-user/reducers/user-slice';

const useUserRadioSelect = () => {
  const dispatch = useAppDispatch();
  const { users, userQuery, isDataFetched } = useAppState(
    (state) => state.users
  );

  const updateMainSortState = (
    sortColumnName: string,
    sortColumnDirection: string
  ) => {
    dispatch(
      setUserQuery({
        ...userQuery,
        sortColumnName,
        sortColumnDirection,
      })
    );
  };

  const updateMainPagingState = (page: number) => {
    dispatch(
      setUserQuery({
        ...userQuery,
        pageNumber: page,
      })
    );
  };

  const { hasSortColumn, handleSort } = useAppSort(
    DEFAULT_MANAGE_USER_SORT_COLUMN,
    updateMainSortState
  );
  const { handlePaging } = useAppPaging(updateMainPagingState);

  const handleSearch = (searchTerm: string) => {
    dispatch(
      setUserQuery({
        ...userQuery,
        searchTerm: searchTerm.trim(),
        pageNumber: 1,
      })
    );
  };

  // Fetch Data
  useEffect(() => {
    if (!isDataFetched) {
      dispatch(getUsers(userQuery));
    }
  }, [dispatch, userQuery, isDataFetched]);

  useEffect(() => {
    return () => {
      dispatch(resetUserSlice());
    };
  }, [dispatch]);

  return {
    hasSortColumn,
    users,
    searchTerm: userQuery.searchTerm,
    handleSort,
    handlePaging,
    handleSearch,
    sortState: {
      name: hasSortColumn.sortColumn,
      orderBy: hasSortColumn.sortOrder,
    },
  };
};

export default useUserRadioSelect;
