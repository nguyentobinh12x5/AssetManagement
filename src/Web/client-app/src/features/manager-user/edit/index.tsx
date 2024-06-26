import CreateUpdateUserForm from "../components/CreateUpdateUserForm";
import useEditForm from "./useEditUserForm";
import { IUserForm, UserSchema } from "../components/validateUserSchemas";
const EditUser = () => {
  const { user, handleSubmit } = useEditForm();
  return (
    <div className="user-form">
      <div className="mb-3">
        <h3 className="primaryColor fw-bold fs-5">Edit User</h3>
      </div>
      <CreateUpdateUserForm<IUserForm>
        initialValues={user}
        handleSubmit={handleSubmit}
        validationSchema={UserSchema}
        enableReinitialize={true}
        isEdit={true}
      />
    </div>
  );
};

export default EditUser;
