import React, { useState } from "react";
import IColumnOption from "../../../components/table/interfaces/IColumnOption";
import IPagination from "../../../components/table/interfaces/IPagination";
import ISortState from "../../../components/table/interfaces/ISortState";
import { IPagedModel } from "../../../interfaces/IPagedModel";
import { IBriefUser } from "../interfaces/IBriefUser";
import Table from "../../../components/table/Table";
import ButtonIcon from "../../../components/ButtonIcon";
import { useNavigate } from "react-router-dom";
import { PencilFill } from "react-bootstrap-icons";
import PopupComponent from "../details";
import Loading from "../../../components/Loading";
import ConfirmDisable from "../components/ConfirmDisable";

type UserTableProps = {
  users: IPagedModel<IBriefUser>;
  handleSort: (value: string) => void;
  handlePaging: (page: number) => void;
  sortState: ISortState;
};

const UserTable: React.FC<UserTableProps> = ({
  users,
  sortState,
  handleSort,
  handlePaging,
}) => {
  const { items, pageNumber, totalPages } = users;

  const columns: IColumnOption[] = [
    { name: "Staff Code", value: "StaffCode" },
    { name: "Full Name", value: "FirstName" },
    { name: "Username", value: "UserName", disable: true },
    { name: "Joined Date", value: "JoinDate" },
    { name: "Type", value: "Type" },
  ];
  const navigate = useNavigate();
  const handleEditClick = (userId: string) => {
    navigate(`edit/${userId}`);
  };

  const [selectedUser, setSelectedUser] = useState<string | null>(null);
  const [showPopup, setShowPopup] = useState(false);

  const handleShowPopup = (userId: string) => {
    setSelectedUser(userId);
    setShowPopup(true);
  };

  const handleClosePopup = () => {
    setShowPopup(false);
    setSelectedUser(null);
  };

  const pagination: IPagination = {
    currentPage: pageNumber,
    totalPage: totalPages,
    handleChange: handlePaging,
  };

  if (!users) {
    return <Loading />;
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
          <tr
            key={data.id}
            onClick={() => handleShowPopup(data.id)}
          >
            <td>{data.staffCode}</td>
            <td>{data.fullName}</td>
            <td>{data.userName}</td>
            <td>{data.joinDate.toString()}</td>
            <td>{data.type}</td>
            <div className="text-center d-flex justify-content-center align-items-center gap-2">
              <ButtonIcon
                onClick={(e: { stopPropagation: () => void }) => {
                  e.stopPropagation();
                  handleEditClick(data.id);
                }}
                disable={false}
              >
                <PencilFill />
              </ButtonIcon>

              <div onClick={(e) => e.stopPropagation()}>
                <ConfirmDisable userId={data.id} />
              </div>
            </div>
          </tr>
        ))}
      </Table>
      {selectedUser && (
        <PopupComponent
          show={showPopup}
          handleClose={handleClosePopup}
          userId={selectedUser}
        />
      )}
    </>
  );
};

export default UserTable;
