import { Form, Formik } from "formik";
import useUserForm from "./useUserForm";
import { Button } from "../../../components";
import TextField from "../../../components/formInputs/TextField";
import DateField from "../../../components/formInputs/DateField";
import RadioButtonField from "../../../components/formInputs/RadioButtonField";
import SelectField from "../../../components/formInputs/SelectField";
import { GenderOptions } from "../constants/gender-user";
import { TypeOptions } from "../constants/type-user";
import { useNavigate } from "react-router-dom";
import Loading from "../../../components/Loading";
const UseForm = () => {
  const { user, isLoading, handleSubmit, UserSchema } = useUserForm();
  const navigate = useNavigate();

  if (isLoading) {
    return <Loading />;
  }

  return (
    <Formik
      initialValues={user}
      onSubmit={handleSubmit}
      validationSchema={UserSchema}
      enableReinitialize
    >
      {({ isValid, dirty }) => (
        <Form>
          <div className="mb-3">
            <h3 className="primaryColor fw-bold fs-5">Edit User</h3>
          </div>
          <div className="mb-3">
            <TextField
              id="firstName"
              label="First Name"
              name="firstName"
              type="text"
              required
            />
          </div>
          <div className="mb-3">
            <TextField
              id="lastName"
              label="Last Name"
              name="lastName"
              type="text"
              required
            />
          </div>
          <div className="mb-3">
            <DateField
              id="dateOfBirth"
              label="Date of Birth"
              name="dateOfBirth"
              required
            />
          </div>
          <div className="mb-3">
            <RadioButtonField
              id="gender"
              name="gender"
              label="Gender"
              required
              options={GenderOptions}
              checked
            />
          </div>
          <div className="mb-3">
            <DateField
              id="joinDate"
              label="Joined Date"
              name="joinDate"
              required
            />
          </div>
          <div className="mb-3">
            <SelectField
              id="type"
              label="Type"
              name="type"
              options={TypeOptions}
              required
            />
          </div>
          <div className="d-flex justify-content-end">
            <Button
              type="submit"
              className="btn btn-danger me-4"
              disabled={!(isValid && dirty)}
            >
              Save
            </Button>
            <Button
              type="button"
              className="btn btn-secondary"
              onClick={() => navigate("/user")}
            >
              Cancel
            </Button>
          </div>
        </Form>
      )}
    </Formik>
  );
};

export default UseForm;
