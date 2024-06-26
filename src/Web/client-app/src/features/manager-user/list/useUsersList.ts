import { useEffect, useState } from 'react';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import useAppPaging from '../../../hooks/paging/useAppPaging';
import useAppSort from '../../../hooks/paging/useAppSort';
import { DEFAULT_MANAGE_USER_SORT_COLUMN } from '../constants/user-sort';
import {
  getUsers,
  setUserQuery,
  setIsDataFetched,
} from '../reducers/user-slice';

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
    dispatch(setIsDataFetched(false));
  };

  const updateMainPagingState = (page: number) => {
    dispatch(
      setUserQuery({
        ...userQuery,
        pageNumber: page,
      })
    );
    dispatch(setIsDataFetched(false));
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
    dispatch(
      setUserQuery({
        ...userQuery,
        types: type,
        pageNumber: 1,
      })
    );
    dispatch(setIsDataFetched(false));
  };

  const handleSearch = (searchTerm: string) => {
    setFilterType(null);
    setSearchTerm(searchTerm.trim());
    dispatch(
      setUserQuery({
        ...userQuery,
        searchTerm: searchTerm.trim(),
        pageNumber: 1,
      })
    );
    dispatch(setIsDataFetched(false));
  };

  // Fetch Data
  useEffect(() => {
    console.log('test1');
    if (!isDataFetched) {
      console.log('test2' + userQuery.types);
      dispatch(getUsers(userQuery));
    }
  }, [dispatch, userQuery, isDataFetched, filterType, searchTerm]);

  return {
    hasSortColumn,
    users,
    searchTerm,
    handleSort,
    handlePaging,
    handleFilterByType,
    handleSearch,
  };
};

export default useUserList;
