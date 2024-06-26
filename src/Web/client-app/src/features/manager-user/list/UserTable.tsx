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
import "../../../components/table/CustomTable.scss";
import { Tooltip } from "react-bootstrap";
import TextWithTooltip from "../../../components/table/helper/TextToolTip";
import { formatDate } from "../../../utils/dateUtils";

type UserTableProps = {
  users: IPagedModel<IBriefUser>;
  handleSort: (value: string) => void;
  handlePaging: (page: number) => void;
  sortState: ISortState;
  searchTerm: string;
};

const UserTable: React.FC<UserTableProps> = ({
  users,
  sortState,
  searchTerm,
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
          <tr key={data.id} onClick={() => handleShowPopup(data.id)}>
            <td className="smlsize">{data.staffCode}</td>
            <td className="lrgsize" >
              <TextWithTooltip text={data.fullName} />
            </td>
            <td className="lrgsize">
              <TextWithTooltip text={data.userName} />
            </td>
            <td className="lrgsize">
              <TextWithTooltip text={formatDate(data.joinDate)} />
            </td>
            <td>
              <TextWithTooltip text={data.type.slice(0, 5)} />
            </td>
            <td className="action">
              <div className="d-flex gap-4 justify-content-evenly align-items-center">
                <ButtonIcon
                  onClick={() => handleEditClick(data.id)}
                  disable={false}
                >
                  <PencilFill />
                </ButtonIcon>
                <ConfirmDisable userId={data.id} />
              </div>
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
