import React, { useEffect, useState } from "react";
import { Modal, Button } from "react-bootstrap";
import { Form, Formik, FormikProps } from "formik";
import useChangePassword from "./useChangePassword";
import TextFieldPassword from "../../../components/formInputs/TextFieldPassword";

interface ChangePasswordFormProps {
  show: boolean;
  onHide: () => void;
}

const ChangePasswordForm: React.FC<ChangePasswordFormProps> = ({
  show,
  onHide,
}) => {
  const {
    initialValues,
    handleSubmit,
    ChangePasswordSchema,
    success,
    apiError,
    resetState,
  } = useChangePassword();
  const [shouldResetForm, setShouldResetForm] = useState(false);

  useEffect(() => {
    if (show) {
      resetState();
      setShouldResetForm(true);
    }
  }, [show, resetState]);

  return (
    <Modal
      show={show}
      centered
      className={success ? "changepassword-form-success-wrapper" : ""}
    >
      <div className="changepassword-form-header">
        <h3 className="primaryColor fw-bold fs-5">Change password</h3>
      </div>
      <Modal.Body className="p-0">
        {success ? (
          <div className="changepassword-form-wrapper">
            <div>Your password has been changed successfully!</div>
            <div className="d-flex mt-3 justify-content-end">
              <Button className="btn-light btn-outline-dark" onClick={onHide}>
                Close
              </Button>
            </div>
          </div>
        ) : (
          <Formik
            initialValues={initialValues}
            onSubmit={handleSubmit}
            validationSchema={ChangePasswordSchema}
            enableReinitialize={shouldResetForm}
          >
            {(formikProps: FormikProps<any>) => {
              const { isValid, dirty } = formikProps;

              if (shouldResetForm) {
                setShouldResetForm(false);
              }

              return (
                <Form>
                  <div className="changepassword-form-wrapper">
                    <TextFieldPassword
                      id="oldPassword"
                      label="Old Password"
                      name="currentPassword"
                      required
                      aria-describedby="currentPasswordError"
                      apiError={apiError.currentPassword}
                    />

                    <TextFieldPassword
                      id="newPassword"
                      label="New Password"
                      name="newPassword"
                      required
                      aria-describedby="newPasswordError"
                      apiError={apiError.newPassword}
                    />

                    <div className="d-flex gap-3 justify-content-end mt-3">
                      <Button
                        type="submit"
                        className="btn-danger"
                        disabled={!(isValid && dirty)}
                      >
                        Save
                      </Button>
                      <Button variant="secondary" onClick={onHide}>
                        Cancel
                      </Button>
                    </div>
                  </div>
                </Form>
              );
            }}
          </Formik>
        )}
      </Modal.Body>
    </Modal>
  );
};

export default ChangePasswordForm;
