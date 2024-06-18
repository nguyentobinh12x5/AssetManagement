import React, { lazy } from "react";
import { Route, Routes } from "react-router-dom";

import { TODO_ITEM_LIST } from "./constants/todo-item-pages";

const ListTodoItems = lazy(() => import("./list"));

const TodoItems = () => {
  return (
    <Routes>
      <Route path={TODO_ITEM_LIST} element={<ListTodoItems />} />
    </Routes>
  );
};

export default TodoItems;
