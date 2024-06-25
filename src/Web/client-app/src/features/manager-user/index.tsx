import React, { lazy } from "react";
import { Route, Routes } from "react-router-dom";
import { MANAGE_USER_LIST } from "./constants/user-pages";
import { CREATE_USER, EDIT_USER } from "./constants/edit-user";
import CreateUser from "./create";

const EditUser = lazy(() => import("./edit"));
const ListUsers = lazy(() => import("./list"));

const Users = () => {
  return (
    <Routes>
      <Route path={MANAGE_USER_LIST} element={<ListUsers />} />
      <Route path={CREATE_USER} element={<CreateUser />} />
      <Route path={EDIT_USER} element={<EditUser />} />
    </Routes>
  );
};

export default Users;
