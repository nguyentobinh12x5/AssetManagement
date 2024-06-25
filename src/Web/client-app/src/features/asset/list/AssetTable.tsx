import React, { useState } from "react";
import IColumnOption from "../../../components/table/interfaces/IColumnOption";
import IPagination from "../../../components/table/interfaces/IPagination";
import ISortState from "../../../components/table/interfaces/ISortState";
import { IPagedModel } from "../../../interfaces/IPagedModel";
import Table from "../../../components/table/Table";
import ButtonIcon from "../../../components/ButtonIcon";
import { useNavigate } from "react-router-dom";
import { PencilFill, XCircle } from "react-bootstrap-icons";
import Loading from "../../../components/Loading";
import { IBriefAsset } from "../interfaces/IBriefAsset";

type AssetTableProps = {
  assets: IPagedModel<IBriefAsset>;
  handleSort: (value: string) => void;
  handlePaging: (page: number) => void;
  sortState: ISortState;
  searchTerm: string;
};

const AssetTable: React.FC<AssetTableProps> = ({
  assets,
  sortState,
  searchTerm,
  handleSort,
  handlePaging,
}) => {
  const { items, pageNumber, totalPages } = assets;

  const columns: IColumnOption[] = [
    { name: "Asset Code", value: "Code" },
    { name: "Asset Name", value: "Name" },
    { name: "Category", value: "CategoryName" },
    { name: "State", value: "AssetStatusName" },
  ];
  const navigate = useNavigate();
  const handleEditClick = (assetId: string) => {
    // Handle navigate(`edit/${assetId}`);
  };

  const [selectedAsset, setSelectedAsset] = useState<string | null>(null);
  const [showPopup, setShowPopup] = useState(false);

  const handleShowPopup = (assetId: string) => {
    setSelectedAsset(assetId);
    setShowPopup(true);
  };

  const handleClosePopup = () => {
    setShowPopup(false);
    setSelectedAsset(null);
  };

  const pagination: IPagination = {
    currentPage: pageNumber,
    totalPage: totalPages,
    handleChange: handlePaging,
  };

  if (!assets) {
    return <Loading />;
  }

  if (items?.length === 0 && searchTerm) {
    return (
      <div className="text-center">
        <p>There's no data, please adjust your search condition</p>
      </div>
    );
  }

  if (items?.length === 0) {
    return (
      <div className="text-center">
        <p>No data available</p>
      </div>
    );
  }

  return (
    <>
      <Table
        columns={columns}
        sortState={sortState}
        handleSort={handleSort}
        pagination={pagination}
      >
        {items?.map((data) => (
          <tr key={data.id}>
            <td>{data.code}</td>
            <td onClick={() => handleShowPopup(data.id)}>{data.name}</td>
            <td>{data.categoryName}</td>
            <td>{data.assetStatusName}</td>
            <td className="text-center d-flex justify-content-center align-items-center gap-2 border-0">
              <div>
                <ButtonIcon
                  onClick={() => {
                    handleEditClick(data.id);
                  }}
                  disable={false}
                >
                  <PencilFill />
                </ButtonIcon>
              </div>

              <div>
                <ButtonIcon>
                  <XCircle color="red" />
                </ButtonIcon>
              </div>
            </td>
          </tr>
        ))}
      </Table>
      {selectedAsset && <>{/* PopupComponent View Detail */}</>}
    </>
  );
};

export default AssetTable;
