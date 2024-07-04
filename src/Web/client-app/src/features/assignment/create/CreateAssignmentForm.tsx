import React from "react";
import useCreateAssignment from "./useCreateAssignment";
import CreateUpdateAssignmentForm from "../components/CreateUpdateAssignmentForm";

const CreateAssignmentForm = () => {
  const { handleSubmit, initialValues, valdationSchema } =
    useCreateAssignment();

  return (
    <div>
      <CreateUpdateAssignmentForm
        initialValues={initialValues}
        handleSubmit={handleSubmit}
        validationSchema={valdationSchema}
      />
    </div>
  );
};

export default CreateAssignmentForm;
