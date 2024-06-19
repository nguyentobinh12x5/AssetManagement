import useFirstTimeLogin from "./useFirstTimeLogin";
import ChangePasswordFirstimeForm from "./ChangePasswordFirstTimeForm";

const FirtimeLoginChangePassword = () => {
  const { initialValues, handleSubmit, ChangePasswordFirstimeSchema } =
    useFirstTimeLogin();

  return (
    <div className="firstime-login-wrapper">
      <div
        className="modal show firstime-login-container"
        style={{ display: "block", position: "initial" }}
      >
        <ChangePasswordFirstimeForm
          formInitialValues={initialValues}
          handleChangePassword={handleSubmit}
          validationSchema={ChangePasswordFirstimeSchema}
        />
      </div>
    </div>
  );
};

export default FirtimeLoginChangePassword;
