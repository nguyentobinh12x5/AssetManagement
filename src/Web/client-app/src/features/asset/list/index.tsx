import React from "react";
import { Button, Col, Row } from "react-bootstrap";
import AssetTable from "./AssetTable";
import { useNavigate } from "react-router-dom";
import FilterByStatus from "../components/FilterByStatus";
import FilterByCategory from "../components/FilterByCategory";
import SearchBox from "../components/SearchBox";
import useAssetList from "./useAssetList";
import "./AssetList.scss";
import DetailForm from "../detail/DetailForm";
import { CREATE_ASSET_PATH } from "../constants/create-asset";

const AssetList = () => {
  const {
    hasSortColumn,
    assets,
    searchTerm,
    handleSort,
    handlePaging,
    handleFilterByCategory,
    handleFilterByStatus,
    handleSearch,
  } = useAssetList();

  const navigate = useNavigate();

  const handleCreateAsset = () => {
    // Navigate to Create new Asset
    navigate(CREATE_ASSET_PATH);
  };

  return (
    <div className="asset-list">
      <p className="title">Asset list</p>

      <Row className="mb-3">
        <Col md={3}>
          <FilterByStatus handleFilterByStatus={handleFilterByStatus} />
        </Col>

        <Col md={3}>
          <FilterByCategory handleFilterByCategory={handleFilterByCategory} />
        </Col>

        <Col md={3} className="ml-auto">
          <SearchBox handleFilterBySearchTerm={handleSearch} />
        </Col>

        <Col md={3}>
          <Button variant="danger" onClick={handleCreateAsset}>
            Create new asset
          </Button>
        </Col>
      </Row>

      <AssetTable
        assets={assets}
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

export default AssetList;
