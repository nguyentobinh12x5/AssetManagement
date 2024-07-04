/* eslint-disable @typescript-eslint/no-unused-vars */
import React, { useState } from "react";
import IColumnOption from "../../../components/table/interfaces/IColumnOption";
import IPagination from "../../../components/table/interfaces/IPagination";
import ISortState from "../../../components/table/interfaces/ISortState";
import { IPagedModel } from "../../../interfaces/IPagedModel";
import Table from "../../../components/table/Table";
import ButtonIcon from "../../../components/ButtonIcon";
import { useNavigate } from "react-router-dom";
import {
  PencilFill,
  XCircle,
  ArrowCounterclockwise,
} from "react-bootstrap-icons";
import Loading from "../../../components/Loading";
import { IBriefAssignment } from "../interfaces/IBriefAssignment";
import {
  AssignmentState,
  WATTING_FOR_ACCEPTANCE,
} from "../constants/assignment-state";
import { formatDate } from "../../../utils/dateUtils";
import DetailForm from "../detail/DetailForm";
import ConfirmDelete from "../delete/deleteAssignmentModal";
import { calculateNo, PaginationInfo } from "../../../utils/appUtils";

const columns: IColumnOption[] = [
  { name: "No.", value: "Id" },
  { name: "Asset Code", value: "Asset.Code" },
  { name: "Asset Name", value: "Asset.Name" },
  { name: "Assigned To", value: "AssignedTo" },
  { name: "Assigned By", value: "AssignedBy" },
  { name: "Assigned Date", value: "AssignedDate" },
  { name: "State", value: "State" },
];

type AssignmentTableProps = {
  assignments: IPagedModel<IBriefAssignment>;
  handleSort: (value: string) => void;
  handlePaging: (page: number) => void;
  sortState: ISortState;
  searchTerm: string;
  sortColumnDirection: string;
};

const AssignmentTable: React.FC<AssignmentTableProps> = ({
  assignments,
  sortState,
  searchTerm,
  handleSort,
  handlePaging,
  sortColumnDirection,
}) => {
  const { items, pageNumber, totalPages } = assignments;

  const navigate = useNavigate();
  const handleEditClick = (assetId: string) => {
    // Handle navigate(`edit/${assetId}`);
  };
  const [idDelete, setIdDelete] = useState<number | null>(null);
  const [selectedAssignment, setSelectedAssignment] = useState<string | null>(
    null
  );
  const handleConfirmDelete = (id: string) => {
    const numId = parseInt(id);
    setIdDelete(numId);
  };
  const handleCloseConfirmDelete = () => {
    setIdDelete(null);
  };
  const handleShowDetail = (id: string) => {
    setSelectedAssignment(id);
  };

  const handleClosePopup = () => {
    setSelectedAssignment(null);
  };

  const pagination: IPagination = {
    currentPage: pageNumber,
    totalPage: totalPages,
    handleChange: handlePaging,
  };

  if (!assignments) {
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
        {items?.map((data, index) => {
          const paginationInfo: PaginationInfo = {
            pageNumber: pageNumber,
            pageSize: 5,
            totalPages: totalPages,
            sortDirection: sortColumnDirection,
          };
          return (
            <tr key={data.id} onClick={handleShowDetail.bind(null, data.id)}>
              <td>{calculateNo(index, paginationInfo)}</td>
              <td>{data.assetCode}</td>
              <td>{data.assetName}</td>
              <td>{data.assignedTo}</td>
              <td>{data.assignedBy}</td>
              <td>{formatDate(data.assignedDate)}</td>
              <td>{AssignmentState[data.state]}</td>
              <td className="action" onClick={(e) => e.stopPropagation()}>
                <div className="d-flex gap-3 justify-content-evenly align-items-center">
                  <ButtonIcon
                    onClick={() => {
                      handleEditClick(data.id);
                    }}
                    disable={false}
                  >
                    <PencilFill />
                  </ButtonIcon>
                  <ButtonIcon>
                    <XCircle color="red" />
                  </ButtonIcon>
                  <ButtonIcon>
                    <ArrowCounterclockwise color="blue" />
                  </ButtonIcon>
                </div>
              </td>
            </tr>
          );
        })}
      </Table>
      {idDelete && (
        <ConfirmDelete Id={idDelete} hideModal={handleCloseConfirmDelete} />
      )}
      {selectedAssignment && (
        <DetailForm
          id={parseInt(selectedAssignment)}
          onClose={handleClosePopup}
        />
      )}
    </>
  );
};

export default AssignmentTable;
