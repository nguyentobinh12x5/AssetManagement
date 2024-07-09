import { Col, Row } from "react-bootstrap";
import SearchBox from "../../../components/SearchBox/SearchBox";
import "./ReturningList.scss";
import ReturningTable from "./ReturingTable";
import useReturningList from "./useReturningList";
import FilterByState from "../components/FilterByState";
import FilterByReturnedDate from "../components/FilterByReturnedDate";

const ReturningList = () => {
  const {
    hasSortColumn,
    searchTerm,
    handleSort,
    handlePaging,
    handleSearch,
    returnings,
    sortColumnDirection,
  } = useReturningList();

  return (
    <div className="asset-list offset-1">
      <p className="title">Request list</p>

      <Row className="mb-3">
        <Col md={3}>
          <FilterByState />
        </Col>
        <Col md={3}>
          <FilterByReturnedDate />
        </Col>
        <Col md={3}></Col>
        <Col md={3}>
          <SearchBox
            defaultValue={searchTerm}
            handleFilterBySearchTerm={handleSearch}
          />
        </Col>
      </Row>

      <ReturningTable
        returnings={returnings}
        searchTerm={searchTerm}
        sortState={{
          name: hasSortColumn.sortColumn,
          orderBy: hasSortColumn.sortOrder,
        }}
        sortColumnDirection={sortColumnDirection}
        handleSort={handleSort}
        handlePaging={handlePaging}
      />
    </div>
  );
};

export default ReturningList;
