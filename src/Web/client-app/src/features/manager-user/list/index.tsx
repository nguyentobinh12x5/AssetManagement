import { Button, Col, Row } from "react-bootstrap";
import DropdownFilter from "../../../components/dropdownFilter/DropDownFilter";
import FilterByRole from "../components/FilterByRole";
import SearchBox from "../components/SearchBox";
import UserTable from "./UserTable";
import useUserList from "./useUsersList";
import Loading from "../../../components/Loading";
import { useNavigate } from "react-router-dom";

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

  const navigate = useNavigate();

  const handleCreateUser = () => {
    navigate(`create`);
  }

  return (
    <div>
      <div className="">
        <Row className="mb-3">
          <Col md={2}>
            <FilterByRole handleFilterByType={handleFilterByType} />
          </Col>
          <Col md={3}>
            <SearchBox handleFilterBySearchTerm={handleSearch} />
          </Col>
          <Col md={3}>
            <Button onClick={() => handleCreateUser()}>Create new user</Button>
          </Col>
        </Row>
      </div>

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
