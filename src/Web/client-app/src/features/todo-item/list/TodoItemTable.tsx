import React from "react";
import IColumnOption from "../../../components/table/interfaces/IColumnOption";
import IPagination from "../../../components/table/interfaces/IPagination";
import ISortState from "../../../components/table/interfaces/ISortState";
import { IPagedModel } from "../../../interfaces/IPagedModel";
import { ITodoItem } from "../interfaces/ITodoItem";
import Table from "../../../components/table/Table";

type TodoItemTableProps = {
  todoItems: IPagedModel<ITodoItem>;
  handleSort: (value: string) => void;
  handlePaging: (page: number) => void;
  sortState: ISortState;
};

const TodoItemTable: React.FC<TodoItemTableProps> = ({
  todoItems,
  sortState,
  handleSort,
  handlePaging,
}) => {
  const { items, pageNumber, totalPages } = todoItems;

  const columns: IColumnOption[] = [
    { name: "Id", value: "Id" },
    { name: "List Id", value: "ListId" },
    { name: "Title", value: "Title" },
    { name: "Done", value: "Done" },
  ];

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
          <td>{data.id}</td>
          <td>{data.listId}</td>
          <td>{data.title}</td>
          <td>{data.done}</td>
        </tr>
      ))}
    </Table>
  );
};

export default TodoItemTable;
