import React, { useMemo, useRef, useState } from "react";
import { IPagedModel } from "../../../interfaces/IPagedModel";
import ISortState from "../../../components/table/interfaces/ISortState";
import IColumnOption from "../../../components/table/interfaces/IColumnOption";
import Table from "../../../components/table/Table";
import IPagination from "../../../components/table/interfaces/IPagination";
import ButtonIcon from "../../../components/ButtonIcon";
import { ArrowCounterclockwise, CheckLg, XLg } from "react-bootstrap-icons";
import { IMyAssignmentBrief } from "../interfaces/IMyAssignment";
import { formatDate } from "../../../utils/dateUtils";
import "./MyAssignmentsTable.scss";
import DetailForm from "../../assignment/detail/DetailForm";
import { AssignmentState } from "../../assignment/constants/assignment-state";
import useResponseAssignment from "./useResponseAssignment";
import useDetailAssignment from "./useDetailAssignment";
import ConfirmAssignmentModal from "../components/ConfirmAssignmentModal";

interface Props {
  assignments: IPagedModel<IMyAssignmentBrief>;
  handleSort: (value: string) => void;
  handlePaging: (page: number) => void;
  sortState: ISortState;
}

const columns: IColumnOption[] = [
  { name: "Asset Code", value: "Asset.Code" },
  { name: "Asset Name", value: "Asset.Name" },
  { name: "Category", value: "Asset.Category.Name" },
  { name: "Assigned Date", value: "AssignedDate" },
  { name: "State", value: "State" },
];

const MyAssignmentsTable: React.FC<Props> = ({
  assignments,
  sortState,
  handleSort,
  handlePaging,
}) => {
  const { items, pageNumber, totalPages } = assignments;
  const { handleClosePopup, handleShowDetail, selectedAssignment } =
    useDetailAssignment();
  const {
    currentSelectedAssignment,
    typeModal,
    handleShowAcceptModal,
    handleShowDeclineModal,
    hideDisableModal,
    handleShowReturningRequestModal,
  } = useResponseAssignment();

  const pagination = useMemo<IPagination>(
    () => ({
      currentPage: pageNumber,
      totalPage: totalPages,
      handleChange: handlePaging,
    }),
    [handlePaging, pageNumber, totalPages]
  );

  if (!items?.length) {
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
          <tr key={data.id} onClick={handleShowDetail.bind(null, data.id)}>
            <td>{data.assetCode}</td>
            <td>{data.assetName}</td>
            <td>{data.categoryName}</td>
            <td>{formatDate(data.assignedDate)}</td>
            <td>{AssignmentState[data.state]}</td>
            <td className="action" onClick={(e) => e.stopPropagation()}>
              <div className="d-flex gap-2 justify-content-evenly align-items-center">
                <ButtonIcon
                  onClick={() => {
                    handleShowAcceptModal(data.id);
                  }}
                  disable={
                    !(data.state === AssignmentState["Waiting for acceptance"])
                  }
                >
                  <CheckLg
                    color="#cf2338"
                    stroke="#cf2338"
                    strokeWidth={1.5}
                    size={20}
                  />
                </ButtonIcon>
                <ButtonIcon
                  disable={
                    !(data.state === AssignmentState["Waiting for acceptance"])
                  }
                  onClick={() => handleShowDeclineModal(data.id)}
                >
                  <XLg color="gray" stroke="gray" strokeWidth={1.5} size={20} />
                </ButtonIcon>
                <ButtonIcon
                  disable={
                    !(data.state === AssignmentState.Accepted)
                  }
                  onClick={() => handleShowReturningRequestModal(data.id)}
                >
                  <ArrowCounterclockwise
                    color="blue"
                    stroke="blue"
                    strokeWidth={1.5}
                    size={20}
                  />
                </ButtonIcon>
              </div>
            </td>
          </tr>
        ))}
      </Table>

      <ConfirmAssignmentModal
        assignmentId={currentSelectedAssignment}
        hideModal={hideDisableModal}
        typeModal={typeModal}
      />

      {selectedAssignment && (
        <DetailForm id={selectedAssignment} onClose={handleClosePopup} />
      )}
    </>
  );
};

export default MyAssignmentsTable;
