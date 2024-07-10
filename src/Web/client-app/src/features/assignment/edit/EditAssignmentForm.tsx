import useEditAssignment from "./useEditAssignment";
import CreateUpdateAssignmentForm from "../components/CreateUpdateAssignmentForm";
import { useParams } from "react-router-dom";

const EditAssignmentForm = () => {
  const { id: assignmentId } = useParams();

  const { handleSubmit, initialValues, valdationSchema, isLoading } =
    useEditAssignment(parseInt(assignmentId ?? `0`));

  if (isLoading) {
    return <div>Loading...</div>;
  }

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

export default EditAssignmentForm;
