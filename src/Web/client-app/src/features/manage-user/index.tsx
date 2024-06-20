import React, { lazy } from "react";
import { Route, Routes } from 'react-router-dom';

import { MANAGE_USER_LIST } from "./constants/user-pages";

const ListUsers = lazy(() => import("./list"));

const Users = () => {
    return (
        <Routes>
            <Route path={MANAGE_USER_LIST} element={< ListUsers />} />
        </Routes>
    )
}

export default Users;