import React, { lazy } from "react";
import { Route, Routes } from "react-router-dom";
import { ASSIGNMENT_LIST } from "./constants/assignment-list";
import { CREATE_ASSIGNMENT_PATH } from "./constants/create-assignment";
import { EDIT_ASSIGNMENT_PATH } from "./constants/edit-assignment";

const AssignmentList = lazy(() => import("./list"));
const CreateNewAssignment = lazy(() => import("./create"));
const EditAssignment = lazy(() => import("./edit"));

const Assets = () => {
  return (
    <Routes>
      <Route path={ASSIGNMENT_LIST} element={<AssignmentList />} />
      <Route path={CREATE_ASSIGNMENT_PATH} element={<CreateNewAssignment />} />
      <Route path={EDIT_ASSIGNMENT_PATH} element={<EditAssignment />} />
    </Routes>
  );
};

export default Assets;
