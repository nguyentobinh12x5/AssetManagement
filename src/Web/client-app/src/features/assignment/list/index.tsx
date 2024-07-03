import { Button, Col, Row } from "react-bootstrap";
import AssignmentTable from "./AssignmentTable";
import { useNavigate } from "react-router-dom";
import SearchBox from "../../../components/SearchBox/SearchBox";
import useAssignmentList from "./useAssignmentList";
import "./AssignmentList.scss";
import { CREATE_ASSIGNMENT_PATH } from "../constants/create-assignment";
import FilterByState from "../components/FilterByState";
import FilterByAssignedDate from "../components/FilterByAssignedDate";
const AssignmentList = () => {
  const {
    hasSortColumn,
    searchTerm,
    handleSort,
    handlePaging,
    handleSearch,
    assignments,
  } = useAssignmentList();

  const navigate = useNavigate();

  const handleCreateAsset = () => {
    // Navigate to Create new Asset
    navigate(CREATE_ASSIGNMENT_PATH);
  };

  return (
    <div className="asset-list offset-1">
      <p className="title">Assignment list</p>

      <Row className="mb-3">
        <Col md={2}>
          <FilterByState />
        </Col>
        <Col md={2}>
          <FilterByAssignedDate />
        </Col>
        <Col md={{ span: 3, offset: 2 }} className="ml-auto">
          <SearchBox
            defaultValue={searchTerm}
            handleFilterBySearchTerm={handleSearch}
          />
        </Col>

        <Col md={3} className="d-flex justify-content-end align-items-start">
          <Button variant="danger" onClick={handleCreateAsset}>
            Create new assignment
          </Button>
        </Col>
      </Row>

      <AssignmentTable
        assignments={assignments}
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

export default AssignmentList;
