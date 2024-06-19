import useTodoItemList from "./useTodoItemList";
import TodoItemTable from "./TodoItemTable";

const ListTodoItems = () => {
  const {
    defaultIPagedTodoItemModel,
    hasSortColumn,
    todoItems,

    handleSort,
    handlePaging,
  } = useTodoItemList();

  return (
    <TodoItemTable
      todoItems={todoItems ?? defaultIPagedTodoItemModel}
      sortState={{
        name: hasSortColumn.sortColumn,
        orderBy: hasSortColumn.sortOrder,
      }}
      handleSort={handleSort}
      handlePaging={handlePaging}
    />
  );
};

export default ListTodoItems;
