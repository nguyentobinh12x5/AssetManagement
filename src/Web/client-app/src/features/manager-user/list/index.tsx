import { Button, Col, Row } from "react-bootstrap";
import FilterByRole from "../components/FilterByRole";
import SearchBox from "../components/SearchBox";
import UserTable from "./UserTable";
import useUserList from "./useUsersList";
import { useNavigate } from "react-router-dom";
import "./UserList.scss";

const ListUsers = () => {
  const {
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
  };

  return (
    <div className="user-list">
      <p className="title">User list</p>

      <Row className="mb-3">
        <Col md={2}>
          <FilterByRole handleFilterByType={handleFilterByType} />
        </Col>

        <Col md={{ span: 4, offset: 4 }} className="ml-auto">
          <SearchBox handleFilterBySearchTerm={handleSearch} />
        </Col>

        <Col md={2}>
          <Button variant="danger" onClick={handleCreateUser}>
            Create new user
          </Button>
        </Col>
      </Row>

      <UserTable
        users={users}
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
