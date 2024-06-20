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
import { Button } from "../../../components";
import Loading from "../../../components/Loading";

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
    { name: "Username", value: "UserName" },
    { name: "Joined Date", value: "JoinDate" },
    { name: "Type", value: "Type" },
  ];

  const navigate = useNavigate();

  const handleEditClick = (userId: string) => {
    navigate(`edit/${userId}`);
  };
  const handleCreateUser = () => {
    navigate(`create`);
  }
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
  const formatDate = (date: Date) => {
    const parsedDate = typeof date === 'string' ? new Date(date) : date;
    const day = String(parsedDate.getDate()).padStart(2, '0');
    const month = String(parsedDate.getMonth() + 1).padStart(2, '0');
    const year = parsedDate.getFullYear();
    return `${day}/${month}/${year}`;
  };
  const pagination: IPagination = {
    currentPage: pageNumber,
    totalPage: totalPages,
    handleChange: handlePaging,
  };
  if (!users) {
    return (
       <Loading />
    );
  }
  return (
    <>
      <Button onClick={() => handleCreateUser()}>Create new user</Button>
      <Table
        columns={columns}
        sortState={sortState}
        handleSort={handleSort}
        pagination={pagination}
      >
        {items?.map((data) => (
          <tr key={data.id} onClick={() => handleShowPopup(data.id)} style={{ cursor: 'pointer' }}>
            <td>{data.staffCode}</td>
            <td>{data.fullName}</td>
            <td>{data.userName}</td>
            <td>{formatDate(data.joinDate)}</td>
            <td>{data.type}</td>
            <td>
              <ButtonIcon
                onClick={() => {
                   
                  handleEditClick(data.id);
                }}
                disable={false}
              >
                <PencilFill></PencilFill>
              </ButtonIcon>
            </td>
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
