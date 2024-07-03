import React, { useCallback, useEffect } from "react";
import useAssetRadioSelect from "./useAssetRadioSelect";
import IColumnOption from "../../../../components/table/interfaces/IColumnOption";
import IPagination from "../../../../components/table/interfaces/IPagination";
import Table from "../../../../components/table/Table";

const columns: IColumnOption[] = [
  { name: "", value: "", disable: true },
  { name: "Asset Code", value: "Code" },
  { name: "Asset Name", value: "Name" },
  { name: "Category", value: "Category" },
];

interface Props {
  handleSelect: (value: any) => void;
  selectedValue: any;
}

const AssetRadioSelectTable: React.FC<Props> = ({
  handleSelect,
  selectedValue,
}) => {
  const {
    handlePaging,
    sortState,
    handleSort,
    assets: { items, pageNumber, totalPages },
  } = useAssetRadioSelect();
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
    if (items.length > 0 && !selectedValue) onSelect(items[0]);
  }, [selectedValue, items, onSelect]);

  if (!items.length) return <div>No Available Asset</div>;

  return (
    <Table
      columns={columns}
      sortState={sortState}
      handleSort={handleSort}
      pagination={pagination}
    >
      {items?.map((data, index) => (
        <tr key={data.id}>
          <td>
            <input
              className={`form-check-input input-radio`}
              type="radio"
              name={"select-asset"}
              title="select-asset"
              defaultChecked={
                selectedValue ? selectedValue.id === data.id : index === 0
              }
              onChange={(e) => {
                if (e.target.checked) onSelect(data);
              }}
            ></input>
          </td>
          <td>{data.code}</td>
          <td>{data.name}</td>
          <td>{data.category}</td>
          <td>{data.assetStatus}</td>
        </tr>
      ))}
    </Table>
  );
};

export default AssetRadioSelectTable;
