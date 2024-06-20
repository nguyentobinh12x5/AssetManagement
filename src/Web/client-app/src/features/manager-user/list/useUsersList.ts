import { useCallback, useEffect, useState } from 'react';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import useAppPaging from '../../../hooks/paging/useAppPaging';
import useAppSort from '../../../hooks/paging/useAppSort';
import { DEFAULT_MANAGE_USER_SORT_COLUMN } from '../constants/user-sort';
import {
  getUsers,
  getUsersByType,
  getUsersBySearchTerm,
  setUserQuery,
} from '../reducers/user-slice';

const useUserList = () => {
  const dispatch = useAppDispatch();
  const { users, userQuery } = useAppState((state) => state.users);

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

  const [filterType, setFilterType] = useState<string[] | null>(null);
  const [searchTerm, setSearchTerm] = useState<string>('');

  const handleFilterByType = (type: string[]) => {
    setSearchTerm('');
    setFilterType(type);
    dispatch(
      setUserQuery({
        ...userQuery,
        pageNumber: 1,
      })
    );
  };

  const handleSearch = (searchTerm: string) => {
    setFilterType([]);
    setSearchTerm(searchTerm.trim());
    dispatch(
      setUserQuery({
        ...userQuery,
        pageNumber: 1,
      })
    );
  };

  // Fetch Data
  useEffect(() => {
    const fetchData = () => {
      if (filterType && !searchTerm) {
        dispatch(getUsersByType({ ...userQuery, type: filterType }));
      } else if (searchTerm && !filterType) {
        dispatch(getUsersBySearchTerm({ ...userQuery, searchTerm }));
      } else {
        dispatch(getUsers(userQuery));
      }
    };

    fetchData();
  }, [dispatch, userQuery]);

  return {
    hasSortColumn,
    users,
    handleSort,
    handlePaging,
    handleFilterByType,
    handleSearch,
  };
};

export default useUserList;
