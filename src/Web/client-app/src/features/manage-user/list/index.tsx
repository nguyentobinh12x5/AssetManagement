import UserTable from "./UserTable";
import useUserList from "./useUsersList";

const ListUsers = () => {
    const { 
        defaultIPagedUserModel,
        hasSortColumn,
        users,

        handleSort,
        handlePaging
    } = useUserList();

    return (
        <UserTable
            users={users ?? defaultIPagedUserModel}
            sortState={{ 
                name: hasSortColumn.sortColumn, 
                orderBy: hasSortColumn.sortOrder}}
            handleSort={handleSort}
            handlePaging={handlePaging}
        />
    )
}

export default ListUsers;