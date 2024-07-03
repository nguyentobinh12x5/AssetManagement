import React, { lazy } from "react";
import { Route, Routes } from "react-router-dom";
import { ASSIGNMENT_LIST } from "./constants/assignment-list";
import { CREATE_ASSIGNMENT_PATH } from "./constants/create-assignment";

const AssignmentList = lazy(() => import("./list"));
const CreateNewAssignment = lazy(() => import("./create"));

const Assets = () => {
  return (
    <Routes>
      <Route path={ASSIGNMENT_LIST} element={<AssignmentList />} />
      <Route path={CREATE_ASSIGNMENT_PATH} element={<CreateNewAssignment />} />
    </Routes>
  );
};

export default Assets;
