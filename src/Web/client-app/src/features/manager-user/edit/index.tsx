import EditForm from "./EditForm";
import "./EditForm.scss";
const EditUser = () => {
  return (
    <div className="user-form">
      <div className="mb-3">
        <h3 className="primaryColor fw-bold fs-5">Edit User</h3>
      </div>
      <EditForm />
    </div>
  );
};

export default EditUser;
