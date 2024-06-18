import React, { lazy } from "react";
import { Route, Routes } from "react-router-dom";
import { EDIT_USER } from "./constants/edit-user";

const EditUser = lazy(() => import("./edit"));

const Users = () => {
  return (
    <Routes>
      <Route path={EDIT_USER} element={<EditUser />} />
    </Routes>
  );
};

export default Users;
