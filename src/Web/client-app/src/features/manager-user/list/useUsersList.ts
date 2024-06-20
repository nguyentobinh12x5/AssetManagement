import { useEffect, useState } from 'react';
import { IUserQuery } from '../interfaces/common/IUserQuery';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import useAppPaging from '../../../hooks/paging/useAppPaging';
import useAppSort from '../../../hooks/paging/useAppSort';
import { DEFAULT_MANAGE_USER_SORT_COLUMN } from '../constants/user-sort';
import { IPagedModel } from '../../../interfaces/IPagedModel';
import { APP_DEFAULT_PAGE_SIZE } from '../../../constants/paging';
import {
  getUsers,
  getUsersByType,
  getUsersBySearchTerm,
} from '../reducers/user-slice';
import { IBriefUser } from '../interfaces/IBriefUser';

const defaultIPagedUserModel: IPagedModel<IBriefUser> = {
  items: [],
  pageNumber: 1,
  totalPages: 1,
  totalCount: 0,
  hasPreviousPage: false,
  hasNextpage: false,
};

const useUserList = () => {
  const dispatch = useAppDispatch();
  const { users, isLoading } = useAppState((state) => state.users);

  const updateMainSortState = (
    sortColumnName: string,
    sortColumnDirection: string
  ) => {
    setHasUserQuery((prevQuery) => ({
      ...prevQuery,
      sortColumnName,
      sortColumnDirection,
    }));
  };

  const updateMainPagingState = (page: number) => {
    setHasUserQuery((prevQuery) => ({
      ...prevQuery,
      pageNumber: page,
    }));
  };

  const { hasSortColumn, handleSort } = useAppSort(
    DEFAULT_MANAGE_USER_SORT_COLUMN,
    updateMainSortState
  );
  const { hasPaging, handlePaging } = useAppPaging(updateMainPagingState);

  const defaultUserQuery: IUserQuery = {
    pageNumber: hasPaging.page,
    pageSize: APP_DEFAULT_PAGE_SIZE,
    sortColumnName: hasSortColumn.sortColumn,
    sortColumnDirection: hasSortColumn.sortOrder,
  };

  const [hasUserQuery, setHasUserQuery] = useState(defaultUserQuery);
  const [filterType, setFilterType] = useState<string | null>(null);
  const [searchTerm, setSearchTerm] = useState<string>('');

  // Fetch Data
  useEffect(() => {
    fetchData();
  }, [searchTerm, hasUserQuery, filterType]);

  const fetchData = () => {
    if (filterType && !searchTerm) {
      dispatch(getUsersByType({ ...hasUserQuery, type: filterType }));
    } else if (searchTerm && !filterType) {
      dispatch(getUsersBySearchTerm({ ...defaultUserQuery, searchTerm }));
    } else {
      dispatch(getUsers(hasUserQuery));
    }
  };

  const handleFilterByType = (type: string) => {
    setSearchTerm('');
    setFilterType(type);
    setHasUserQuery((prevQuery) => ({
      ...prevQuery,
      pageNumber: 1,
    }));
  };

  const handleSearch = (searchTerm: string) => {
    setFilterType('');
    setSearchTerm(searchTerm.trim());
  };

  return {
    defaultIPagedUserModel,
    hasSortColumn,
    users,
    isLoading,

    handleSort,
    handlePaging,
    handleFilterByType,
    handleSearch,
  };
};

export default useUserList;
