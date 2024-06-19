import React from "react";
import IColumnOption from "../../../components/table/interfaces/IColumnOption";
import IPagination from "../../../components/table/interfaces/IPagination";
import ISortState from "../../../components/table/interfaces/ISortState";
import { IPagedModel } from "../../../interfaces/IPagedModel";
import { IBriefUser } from "../interfaces/IBriefUser";
import Table from "../../../components/table/Table";
import ButtonIcon from "../../../components/ButtonIcon";
import { useNavigate } from "react-router-dom";
import { PencilFill } from "react-bootstrap-icons";

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

  const pagination: IPagination = {
    currentPage: pageNumber,
    totalPage: totalPages,
    handleChange: handlePaging,
  };

  return (
    <Table
      columns={columns}
      sortState={sortState}
      handleSort={handleSort}
      pagination={pagination}
    >
      {items?.map((data) => (
        <tr key={data.id}>
          <td>{data.staffCode}</td>
          <td>{data.fullName}</td>
          <td>{data.userName}</td>
          <td>{data.joinDate.toString()}</td>
          <td>{data.type}</td>
          <td>
            <ButtonIcon
              onClick={() => handleEditClick(data.id)}
              disable={false}
            >
              <PencilFill></PencilFill>
            </ButtonIcon>
          </td>
        </tr>
      ))}
    </Table>
  );
};

export default UserTable;
