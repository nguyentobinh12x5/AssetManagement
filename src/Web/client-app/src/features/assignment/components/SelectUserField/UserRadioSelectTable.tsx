import React, { useCallback, useEffect } from "react";
import Table from "../../../../components/table/Table";
import useUserRadioSelect from "./useUserRadioSelect";
import IColumnOption from "../../../../components/table/interfaces/IColumnOption";
import IPagination from "../../../../components/table/interfaces/IPagination";
import TextWithTooltip from "../../../../components/table/helper/TextToolTip";

const columns: IColumnOption[] = [
  { name: "", value: "", disable: true },
  { name: "Staff Code", value: "StaffCode" },
  { name: "Full Name", value: "FirstName" },
  { name: "Type", value: "Type" },
];

interface Props {
  handleSelect: (value: any) => void;
  selectedValue?: {
    id: string;
    fullName: string;
  };
}

const UserRadioSelectTable: React.FC<Props> = ({
  handleSelect,
  selectedValue,
}) => {
  const {
    handlePaging,
    sortState,
    searchTerm,
    handleSort,
    users: { items, pageNumber, totalPages },
  } = useUserRadioSelect();
  const pagination: IPagination = {
    currentPage: pageNumber,
    totalPage: totalPages,
    handleChange: handlePaging,
  };

  const onSelect = useCallback(
    (value: any) => {
      handleSelect(value);
    },
    [handleSelect]
  );

  useEffect(() => {
    if (items.length > 0 && !selectedValue)
      onSelect({
        id: items[0].id,
        fullName: items[0].fullName,
      });
  }, [selectedValue, items, onSelect]);

  if (!items.length) return <div>No Available Asset</div>;

  if (items.length === 0 && searchTerm) {
    return (
      <div className="text-center">
        <p>There's no data, please adjust your search condition</p>
      </div>
    );
  }

  return (
    <Table
      columns={columns}
      sortState={sortState}
      handleSort={handleSort}
      pagination={pagination}
    >
      {items?.map((data, index) => (
        <tr key={index}>
          <td>
            <input
              className={`form-check-input input-radio`}
              type="radio"
              name={"select-user"}
              title="select-user"
              checked={
                selectedValue ? selectedValue.id === data.id : index === 0
              }
              onChange={(e) => {
                if (e.target.checked) onSelect(data);
              }}
            ></input>
          </td>
          <td>{data.staffCode}</td>
          <td>
            <TextWithTooltip text={data.fullName} />
          </td>
          <td>
            <TextWithTooltip text={data.type.slice(0, 5)} />
          </td>
        </tr>
      ))}
    </Table>
  );
};

export default UserRadioSelectTable;
