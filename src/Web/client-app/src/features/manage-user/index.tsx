import React, { lazy } from "react";
import { Route, Routes } from 'react-router-dom';

import { MANAGE_USER_LIST } from "./constants/user-pages";

const ListTodoItems = lazy(() => import("./list"));

const TodoItems = () => {
    return (
        <Routes>
            <Route path={MANAGE_USER_LIST} element={< ListTodoItems />} />
        </Routes>
    )
}

export default TodoItems;