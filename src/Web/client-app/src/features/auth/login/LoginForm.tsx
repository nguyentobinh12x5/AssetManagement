import TextField from "../../../components/formInputs/TextField";
import TextFieldPassword from "../../../components/formInputs/TextFieldPassword";
import { Form, Formik } from "formik";
import useLogin from "./useLogin";
import { Button } from "../../../components";

const LoginForm = () => {
  const { initialValues, handleSubmit, LoginSchema } = useLogin();

  return (
    <>
      <Formik
        initialValues={initialValues}
        onSubmit={handleSubmit}
        validationSchema={LoginSchema}
      >
        {({ isSubmitting, isValid, dirty }) => (
          <Form>
            <div className="login-form-header">
              <h3 className="primaryColor fw-bold fs-5">
                Welcome to Online Asset Management
              </h3>
            </div>
            <div className="login-form-wrapper">
              <TextField
                id="username"
                label="Username"
                name="username"
                type="text"
                required
              />

              <TextFieldPassword
                id="password"
                label="Password"
                name="password"
                required
              />
              <Button
                type="submit"
                disabled={isSubmitting || !(isValid && dirty)}
              >
                Login
              </Button>
            </div>
          </Form>
        )}
      </Formik>
    </>
  );
};

export default LoginForm;
