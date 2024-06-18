import { Form, Formik, FormikHelpers } from "formik";
import React from "react";
import { IChangePasswordFirstTime } from "./useFirstTimeLogin";
import TextFieldPassword from "../../../components/formInputs/TextFieldPassword";
import { Button } from "../../../components";

interface Props {
  formInitialValues: IChangePasswordFirstTime;
  handleChangePassword: (
    values: IChangePasswordFirstTime,
    actions: FormikHelpers<IChangePasswordFirstTime>
  ) => void;
}

const ChangePasswordFirstimeForm: React.FC<Props> = ({
  formInitialValues,
  handleChangePassword,
}) => {
  return (
    <Formik initialValues={formInitialValues} onSubmit={handleChangePassword}>
      {({ isValid, dirty }) => (
        <Form>
          <div className="form-header">
            <h3 className="primaryColor fw-bold fs-5 text-start">
              Change password
            </h3>
          </div>
          <div className="form-body change-password-form-body">
            <div className="mb-2">
              <p>This is the first time you logged in.</p>
              <p>You have to change your password to continue.</p>
            </div>

            <TextFieldPassword
              id="newPassword"
              label="New password"
              name="newPassword"
            />
            <Button type="submit" disabled={!(isValid && dirty)}>
              Save
            </Button>
          </div>
        </Form>
      )}
    </Formik>
  );
};

export default ChangePasswordFirstimeForm;
