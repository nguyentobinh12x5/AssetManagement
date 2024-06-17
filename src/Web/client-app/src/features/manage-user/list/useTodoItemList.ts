import { useEffect, useState } from "react";
import { IUser } from "../interfaces/IUser";
import { IUserQuery } from "../interfaces/IUserQuery";
import { useAppDispatch, useAppState } from "../../../redux/redux-hooks";
import { getTodoItems, setTodoItems } from "../reducers/todo-item-slice";
import useAppPaging from "../../../hooks/paging/useAppPaging";
import useAppSort from "../../../hooks/paging/useAppSort";
import { DEFAULT_MANAGE_USER_SORT_COLUMN } from "../constants/user-sort";
import { IPagedModel } from "../../../interfaces/IPagedModel";
import { APP_DEFAULT_PAGE_SIZE, ASCENDING } from "../../../constants/paging";

const defaultIPagedTodoItemModel: IPagedModel<IUser> = {
    items: [],
    pageNumber: 1,
    totalPages: 1,
    totalCount: 0,
    hasPreviousPage: false,
    hasNextpage: false
}

const useUserList = () => {
    // Paging Sorting
    const updateMainSortState = (
        sortColumnName: string,
        sortColumnDirection: string
    ) => {
        setTodoQuery({
            ...hasTodoQuery,
            sortColumnName,
            sortColumnDirection
        });
    }

    const updateMainPagingState = (page: number) => {
        setTodoQuery({
            ...hasTodoQuery,
            pageNumber: page
        });
    }

    const { hasSortColumn, handleSort } = useAppSort(
        DEFAULT_MANAGE_USER_SORT_COLUMN,
        updateMainSortState
    );
    const { hasPaging, handlePaging } = useAppPaging(
        updateMainPagingState
    );

    // Main State
    const dispatch = useAppDispatch();
    const { users } = useAppState((state) => state.);

    const defaultTodoQuery: IUserQuery = {        
        pageNumber: hasPaging.page,
        pageSize: APP_DEFAULT_PAGE_SIZE,
        sortColumnName: hasSortColumn.sortColumn,
        sortColumnDirection: hasSortColumn.sortOrder
    };

    const [ hasTodoQuery, setTodoQuery ] = useState(defaultTodoQuery);

    //Fetch Data
    useEffect(() => {
        dispatch(getTodoItems(hasTodoQuery))
    }, [hasTodoQuery])

    return {
        defaultIPagedTodoItemModel,
        hasSortColumn,
        users,

        handleSort,
        handlePaging
    }

}

export default useUserList;