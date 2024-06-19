import React, { lazy } from "react";
import { Route, Routes } from "react-router-dom";
import { EDIT_USER } from "./constants/edit-user";
import { MANAGE_USER_LIST } from "./constants/user-pages";
const EditUser = lazy(() => import("./edit"));

const ListUsers = lazy(() => import("./list"));
const Users = () => {
  return (
    <Routes>
      <Route path={EDIT_USER} element={<EditUser />} />
      <Route path={MANAGE_USER_LIST} element={<ListUsers />} />
    </Routes>
  );
};

export default Users;
