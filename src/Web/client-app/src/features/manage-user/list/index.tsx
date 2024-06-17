import UserTable from "./UserTable";
import useUserList from "./useTodoItemList";

const ListTodoItems = () => {
    const { 
        defaultIPagedTodoItemModel,
        hasSortColumn,
        users,

        handleSort,
        handlePaging
    } = useUserList();

    return (
        <UserTable
            users={users ?? defaultIPagedTodoItemModel}
            sortState={{ 
                name: hasSortColumn.sortColumn, 
                orderBy: hasSortColumn.sortOrder}}
            handleSort={handleSort}
            handlePaging={handlePaging}
        />
    )
}

export default ListTodoItems;