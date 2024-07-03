import React from "react";
import { Col, Row } from "react-bootstrap";
import { Button } from "../../../components";

const MyAssignmentsTableHeader = () => {
  return (
    <Row className="mb-3">
      <Col md={3}>{/* <FilterByStatus /> */}</Col>

      <Col md={3}>{/* <FilterByCategory /> */}</Col>

      <Col md={{ span: 3, offset: 1 }} className="ml-auto">
        {/* <SearchBox
      defaultValue={searchTerm}
      handleFilterBySearchTerm={handleSearch}
    /> */}
      </Col>

      <Col md={2}>
        <Button variant="danger">Create new asset</Button>
      </Col>
    </Row>
  );
};

export default MyAssignmentsTableHeader;
