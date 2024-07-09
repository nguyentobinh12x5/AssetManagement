/* eslint-disable @typescript-eslint/no-unused-vars */
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
import DetailForm from "../detail/DetailForm";
import DeleteAsset from "../delete/deleteAssetModal";
import ConfirmDisable from "../../manager-user/components/ConfirmDisable";
import ConfirmDelete from "../delete/deleteAssetModal";
import TextWithTooltip from "../../../components/table/helper/TextToolTip";

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
    { name: "Category", value: "Category" },
    { name: "State", value: "AssetStatus" },
  ];

  const handleEditClick = (assetId: string) => {
    setIdAsset(parseInt(assetId));
    setShowPopup(true);
    setTypePopup("edit");
  };

  const [selectedAsset, setSelectedAsset] = useState<string | null>(null);
  const [showPopup, setShowPopup] = useState(false);
  const [idAsset, setIdAsset] = useState<number | null>(null);
  const [typePopup, setTypePopup] = useState<("edit" | "delete") | null>(null);

  const handleConfirmDelete = (id: string) => {
    const numId = parseInt(id);
    setIdAsset(numId);
    setShowPopup(true);
    setTypePopup("delete");
  };
  const handleCloseConfirmDelete = () => {
    setShowPopup(false);
    setIdAsset(null);
    setTypePopup(null);
  };

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

  if (items?.length === 0) {
    return (
      <div className="text-center">
        <p>There's no data, please adjust your search condition</p>
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
          <tr key={data.id} onClick={() => handleShowPopup(data.id)}>
            <td>
              <TextWithTooltip text={data.code} />
            </td>
            <td>
              <TextWithTooltip text={data.name} />
            </td>
            <td>
              <TextWithTooltip text={data.category} />
            </td>
            <td>
              <TextWithTooltip text={data.assetStatus} />
            </td>
            <td className="action" onClick={(e) => e.stopPropagation()}>
              <div className="d-flex gap-3 justify-content-evenly align-items-center">
                <ButtonIcon
                  onClick={() => {
                    handleEditClick(data.id);
                  }}
                  disable={!data.isEnableAction}
                >
                  <PencilFill />
                </ButtonIcon>
                <ButtonIcon
                  onClick={() => handleConfirmDelete(data.id)}
                  disable={!data.isEnableAction}
                >
                  <XCircle color="red" />
                </ButtonIcon>
              </div>
            </td>
          </tr>
        ))}
      </Table>
      {idAsset && showPopup && typePopup && (
        <ConfirmDelete
          Id={idAsset}
          hideModal={handleCloseConfirmDelete}
          typePopup={typePopup}
        />
      )}
      {selectedAsset && showPopup && (
        <DetailForm id={selectedAsset} onClose={handleClosePopup} />
      )}
    </>
  );
};

export default AssetTable;
