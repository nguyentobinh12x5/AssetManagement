import { useEffect, useState } from 'react';
import { ITodoItem } from '../interfaces/ITodoItem';
import { ITodoQuery } from '../interfaces/ITodoQuery';
import { useAppDispatch, useAppState } from '../../../redux/redux-hooks';
import { getTodoItems } from '../reducers/todo-item-slice';
import useAppPaging from '../../../hooks/paging/useAppPaging';
import useAppSort from '../../../hooks/paging/useAppSort';
import { DEFAULT_TODO_ITEM_SORT_COLUMN } from '../constants/todo-item-sort';
import { IPagedModel } from '../../../interfaces/IPagedModel';
import { APP_DEFAULT_PAGE_SIZE } from '../../../constants/paging';

const defaultIPagedTodoItemModel: IPagedModel<ITodoItem> = {
  items: [],
  pageNumber: 1,
  totalPages: 1,
  totalCount: 0,
  hasPreviousPage: false,
  hasNextpage: false,
};

const useTodoItemList = () => {
  // Paging Sorting
  const updateMainSortState = (
    sortColumnName: string,
    sortColumnDirection: string
  ) => {
    setTodoQuery({
      ...hasTodoQuery,
      sortColumnName,
      sortColumnDirection,
    });
  };

  const updateMainPagingState = (page: number) => {
    setTodoQuery({
      ...hasTodoQuery,
      pageNumber: page,
    });
  };

  const { hasSortColumn, handleSort } = useAppSort(
    DEFAULT_TODO_ITEM_SORT_COLUMN,
    updateMainSortState
  );
  const { hasPaging, handlePaging } = useAppPaging(updateMainPagingState);

  // Main State
  const dispatch = useAppDispatch();
  const { todoItems } = useAppState((state) => state.todoItems);

  const defaultTodoQuery: ITodoQuery = {
    listId: 1,
    pageNumber: hasPaging.page,
    pageSize: APP_DEFAULT_PAGE_SIZE,
    sortColumnName: hasSortColumn.sortColumn,
    sortColumnDirection: hasSortColumn.sortOrder,
  };

  const [hasTodoQuery, setTodoQuery] = useState(defaultTodoQuery);

  //Fetch Data
  useEffect(() => {
    dispatch(getTodoItems(hasTodoQuery));
  }, [dispatch, hasTodoQuery]);

  return {
    defaultIPagedTodoItemModel,
    hasSortColumn,
    todoItems,

    handleSort,
    handlePaging,
  };
};

export default useTodoItemList;
