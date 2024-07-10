import React, { useEffect } from "react";
import CreateAssignmentForm from "./CreateAssignmentForm";
import { resetAssetSlice } from "../../asset/reducers/asset-slice";
import { resetUserSlice } from "../../manager-user/reducers/user-slice";
import { resetState } from "../reducers/assignment-detail-slice";
import { useAppDispatch } from "../../../redux/redux-hooks";

const CreateNewAssignment = () => {
  return (
    <div className="asset-form">
      <div className="mb-4">
        <h3 className="primaryColor fw-bold fs-5">Create New Assignment</h3>
      </div>
      <CreateAssignmentForm />
    </div>
  );
};

export default CreateNewAssignment;
