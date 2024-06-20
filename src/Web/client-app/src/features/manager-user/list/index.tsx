import { Col, Row } from "react-bootstrap";
import DropdownFilter from "../../../components/dropdownFilter/DropDownFilter";
import FilterByRole from "../components/FilterByRole";
import SearchBox from "../components/SearchBox";
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
    handleSearch,
  } = useUserList();

  return (
    <div>
      <Row className="mb-3">
        <Col md={3}>
          <FilterByRole handleFilterByType={handleFilterByType} />
        </Col>
        <Col md={3}>
          <SearchBox handleFilterBySearchTerm={handleSearch} />
        </Col>
      </Row>

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
