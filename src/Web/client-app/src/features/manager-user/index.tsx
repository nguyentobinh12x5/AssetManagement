import React, { lazy } from "react";
import { Route, Routes } from "react-router-dom";
import { CREEATE_USER, EDIT_USER } from "./constants/edit-user";
import CreateUser from "./create";

const EditUser = lazy(() => import("./edit"));

const Users = () => {
  return (
    <Routes>
          <Route path={EDIT_USER} element={<EditUser />} />
          <Route path={CREEATE_USER} element={<CreateUser />} />
    </Routes>
  );
};

export default Users;
