import React from "react";
import ColumnICon from "./ColumnIcon";
import IColumnOption from "./interfaces/IColumnOption";
import ISortState from "./interfaces/ISortState";
import IPagination from "./interfaces/IPagination";
import Pagination from "./Pagination";
import { Anchor } from "react-bootstrap";

interface Props {
  columns: IColumnOption[];
  sortState: ISortState;
  handleSort: (value: string) => void;
  children: React.ReactNode;
  pagination: IPagination;
}

const Table: React.FC<Props> = ({
  columns,
  sortState,
  handleSort,
  children,
  pagination,
}) => {
  return (
    <>
      <table className="table">
        <thead className="thead-dark">
          <tr>
            {columns.map((col, i) => (
              <th scope="col" key={i} className={`table-th-${i}`}>
                {!col.disable ? (
                  <Anchor className="btn" onClick={() => handleSort(col.value)}>
                    {col.name}
                    <ColumnICon name={col.value} sortState={sortState} />
                  </Anchor>
                ) : (
                  <span className="btn">{col.name}</span>
                )}
              </th>
            ))}
          </tr>
        </thead>
        <tbody>{children}</tbody>
      </table>

      {!!(pagination && pagination.totalPage && pagination?.totalPage > 0) && (
        <Pagination {...pagination} />
      )}
    </>
  );
};
export default Table;
