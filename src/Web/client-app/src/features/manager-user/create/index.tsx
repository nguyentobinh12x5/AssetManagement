import CreateUpdateUserForm from "../components/CreateUpdateUserForm";
import useCreateUserForm from "./useCreateUserForm";
import { IUserForm, UserSchema } from "../components/validateUserSchemas";

const CreateUser = () => {
  const { handleSubmit, user } = useCreateUserForm();

  return (
    <div className="user-form">
      <div className="mb-3">
        <h3 className="primaryColor fw-bold fs-5">Create New User</h3>
      </div>
      <CreateUpdateUserForm<IUserForm>
        initialValues={user}
        handleSubmit={handleSubmit}
        validationSchema={UserSchema}
        enableReinitialize={true}
      />
    </div>
  );
};

export default CreateUser;
