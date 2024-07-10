import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import useAppPaging from '../../../hooks/paging/useAppPaging';
import useAppSort from '../../../hooks/paging/useAppSort';
import { DEFAULT_MANAGE_USER_SORT_COLUMN } from '../constants/user-sort';
import { setUserQuery } from '../reducers/user-slice';

const useUserList = () => {
  const dispatch = useAppDispatch();
  const { userQuery } = useAppState((state) => state.users);

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

  return {
    hasSortColumn,
    handleSort,
    handlePaging,
    handleFilterByType,
    handleSearch,
  };
};

export default useUserList;
