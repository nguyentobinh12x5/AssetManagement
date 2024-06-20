import CreateForm from "./CreateForm";

const CreateUser = () => {
  return (
    <div className="user-form">
      <div className="mb-3">
        <h3 className="primaryColor fw-bold fs-5">Create New User</h3>
      </div>
      <CreateForm />
    </div>
  );
};

export default CreateUser;
