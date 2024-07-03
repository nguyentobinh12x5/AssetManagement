import React from "react";
import CreateAssignmentForm from "./CreateAssignmentForm";

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
