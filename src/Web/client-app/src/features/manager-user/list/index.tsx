import { Button, Col, Row } from "react-bootstrap";
import FilterByRole from "../components/FilterByRole";
import UserTable from "./UserTable";
import useUserList from "./useUsersList";
import { useNavigate } from "react-router-dom";
import "./UserList.scss";
import SearchBox from "../../../components/SearchBox/SearchBox";
import useFetchUserList from "./useFetchUserList";

const ListUsers = () => {
  const {
    userQuery: { searchTerm },
    users,
  } = useFetchUserList();
  const { hasSortColumn, handleSort, handlePaging, handleSearch } =
    useUserList();

  const navigate = useNavigate();

  const handleCreateUser = () => {
    navigate(`create`);
  };

  return (
    <div className="user-list offset-1">
      <p className="title">User list</p>

      <Row className="mb-3">
        <Col md={3}>
          <FilterByRole />
        </Col>

        <Col md={{ span: 4, offset: 3 }} className="ml-auto">
          <SearchBox
            defaultValue={searchTerm}
            handleFilterBySearchTerm={handleSearch}
          />
        </Col>

        <Col md={2}>
          <Button variant="danger" onClick={handleCreateUser}>
            Create new user
          </Button>
        </Col>
      </Row>

      <UserTable
        users={users}
        searchTerm={searchTerm}
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
