import { Form, Formik } from "formik";
import useCreateForm from "./useCreateForm";
import { Button } from "../../../components";
import TextField from "../../../components/formInputs/TextField";
import DateField from "../../../components/formInputs/DateField";
import RadioButtonField from "../../../components/formInputs/RadioButtonField";
import SelectField from "../../../components/formInputs/SelectField";
import { GenderOptions } from "../constants/gender-user";
import { TypeOptions } from "../constants/type-user";
import { useNavigate } from "react-router-dom";
import "./CreateForm.scss";
import { UserSchema } from "./validateSchema";
const CreateForm = () => {
  const { user, handleSubmit } = useCreateForm();
  const navigate = useNavigate();

  return (
    <Formik
      initialValues={user}
      onSubmit={handleSubmit}
      validateOnChange={true}
      validationSchema={UserSchema}
      enableReinitialize
    >
      {({ isValid, dirty }) => (
        <Form>
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

export default CreateForm;
