import DropdownFilter from "../../../components/dropdownFilter/DropDownFilter";
import FilterByRole from "../components/FilterByRole";
import UserTable from "./UserTable";
import useUserList from "./useUsersList";

const ListUsers = () => {
  const {
    defaultIPagedUserModel,
    hasSortColumn,
    users,

    handleSort,
    handlePaging,
    handleFilterByType,
  } = useUserList();

  return (
    <div>
      <FilterByRole handleFilterByType={handleFilterByType} />

      <UserTable
        users={users ?? defaultIPagedUserModel}
        sortState={{
          name: hasSortColumn.sortColumn,
          orderBy: hasSortColumn.sortOrder,
        }}
        handleSort={handleSort}
        handlePaging={handlePaging}
      />
    </div>
  );
};

export default ListUsers;
