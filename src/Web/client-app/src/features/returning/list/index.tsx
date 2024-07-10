import { Col, Row } from "react-bootstrap";
import SearchBox from "../../../components/SearchBox/SearchBox";
import "./ReturningList.scss";
import ReturningTable from "./ReturingTable";
import useReturningList from "./useReturningList";
import FilterByState from "../components/FilterByState";
import FilterByReturnedDate from "../components/FilterByReturnedDate";
import useFetchReturningList from "./useFetchReturningList";

const ReturningList = () => {
  const { returningQuery, returnings } = useFetchReturningList();
  const {
    hasSortColumn,
    handleSort,
    handlePaging,
    handleSearch,
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
            defaultValue={returningQuery.searchTerm}
            handleFilterBySearchTerm={handleSearch}
          />
        </Col>
      </Row>

      <ReturningTable
        returnings={returnings}
        searchTerm={returningQuery.searchTerm}
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
