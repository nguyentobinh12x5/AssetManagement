import { Form, Formik } from "formik";
import useUserForm from "./useUserForm";
import { Button } from "../../../components";
import TextField from "../../../components/formInputs/TextField";
import DateField from "../../../components/formInputs/DateField";
import RadioButtonField from "../../../components/formInputs/RadioButtonField";
import SelectField from "../../../components/formInputs/SelectField";
const UseForm = () => {
  const { initialValues, handleSubmit, UserSchema } = useUserForm();
  const genderOptions = [
    { id: 1, label: "Male", value: "Male" },
    { id: 2, label: "Female", value: "Female" },
  ];
  const typeOptions = [
    { value: "Admin", label: "Admin" },
    { value: "Staff", label: "Staff" },
  ];
  return (
    <Formik
      initialValues={initialValues}
      onSubmit={handleSubmit}
      validationSchema={UserSchema}
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
              options={genderOptions}
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
              options={typeOptions}
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
              onClick={() => {
                console.log("Cancel");
              }}
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
