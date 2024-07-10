import { useEffect } from 'react';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import useAppPaging from '../../../hooks/paging/useAppPaging';
import useAppSort from '../../../hooks/paging/useAppSort';
import { DEFAULT_MANAGE_USER_SORT_COLUMN } from '../constants/user-sort';
import { getUsers, setUserQuery } from '../reducers/user-slice';

const useUserList = () => {
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

  const handleFilterByType = (type: string[]) => {
    dispatch(
      setUserQuery({
        ...userQuery,
        types: type,
        pageNumber: 1,
      })
    );
  };

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

  return {
    hasSortColumn,
    users,
    searchTerm: !isDataFetched ? userQuery.searchTerm : ``,
    handleSort,
    handlePaging,
    handleFilterByType,
    handleSearch,
  };
};

export default useUserList;
