import useFirstTimeLogin from "./useFirstTimeLogin";
import ChangePasswordFirstimeForm from "./ChangePasswordFirstTimeForm";
import { ChangePasswordFirstimeRequiredSchema } from "./change-password-first-time-schema";

const FirtimeLoginChangePassword = () => {
  const { initialValues, handleSubmit } = useFirstTimeLogin();

  return (
    <div className="firstime-login-wrapper">
      <div
        className="modal show firstime-login-container"
        style={{ display: "block", position: "initial" }}
      >
        <ChangePasswordFirstimeForm
          formInitialValues={initialValues}
          handleChangePassword={handleSubmit}
          validationSchema={ChangePasswordFirstimeRequiredSchema}
        />
      </div>
    </div>
  );
};

export default FirtimeLoginChangePassword;
