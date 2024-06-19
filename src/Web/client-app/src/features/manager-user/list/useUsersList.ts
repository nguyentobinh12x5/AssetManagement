import { useEffect, useState } from 'react';
import { IUserQuery } from '../interfaces/IUserQuery';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import useAppPaging from '../../../hooks/paging/useAppPaging';
import useAppSort from '../../../hooks/paging/useAppSort';
import { DEFAULT_MANAGE_USER_SORT_COLUMN } from '../constants/user-sort';
import { IPagedModel } from '../../../interfaces/IPagedModel';
import { APP_DEFAULT_PAGE_SIZE } from '../../../constants/paging';
import { getUsers } from '../reducers/user-slice';
import { IBriefUser } from "../interfaces/IBriefUser";

const defaultIPagedUserModel: IPagedModel<IBriefUser> = {
  items: [],
  pageNumber: 1,
  totalPages: 1,
  totalCount: 0,
  hasPreviousPage: false,
  hasNextpage: false,
};

const useUserList = () => {
  // Paging Sorting
  const updateMainSortState = (
    sortColumnName: string,
    sortColumnDirection: string
  ) => {
    setUserQuery({
      ...hasUserQuery,
      sortColumnName,
      sortColumnDirection,
    });
  };

  const updateMainPagingState = (page: number) => {
    setUserQuery({
      ...hasUserQuery,
      pageNumber: page,
    });
  };

  const { hasSortColumn, handleSort } = useAppSort(
    DEFAULT_MANAGE_USER_SORT_COLUMN,
    updateMainSortState
  );
  const { hasPaging, handlePaging } = useAppPaging(updateMainPagingState);

  // Main State
  const dispatch = useAppDispatch();
  const { users } = useAppState((state) => state.users);

  const defaultUserQuery: IUserQuery = {
    pageNumber: hasPaging.page,
    pageSize: APP_DEFAULT_PAGE_SIZE,
    sortColumnName: hasSortColumn.sortColumn,
    sortColumnDirection: hasSortColumn.sortOrder,
  };

  const [hasUserQuery, setUserQuery] = useState(defaultUserQuery);

  //Fetch Data
  useEffect(() => {
    dispatch(getUsers(hasUserQuery));
  }, [hasUserQuery]);

  return {
    defaultIPagedUserModel,
    hasSortColumn,
    users,

    handleSort,
    handlePaging,
  };
};

export default useUserList;
