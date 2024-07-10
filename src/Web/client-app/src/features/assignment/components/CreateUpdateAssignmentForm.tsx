import { Form, Formik } from "formik";
import React from "react";
import {
  Button,
  DateField,
  SelectField,
  TextAreaField,
  TextField,
} from "../../../components";
import { ASSIGNMENTS_LINK } from "../../../constants/pages";
import { useNavigate } from "react-router-dom";
import SelectUserField from "./SelectUserField";
import SelectAssetField from "./SelectAssetField";

interface Props {
  initialValues: any;
  handleSubmit: any;
  validationSchema: any;
}

const CreateUpdateAssignmentForm: React.FC<Props> = ({
  initialValues,
  handleSubmit,
  validationSchema,
}) => {
  const navigate = useNavigate();

  return (
    <Formik
      initialValues={initialValues}
      validationSchema={validationSchema}
      onSubmit={handleSubmit}
      enableReinitialize={true}
    >
      {({ dirty, isValid, isSubmitting }) => (
        <Form>
          <div className="mb-3">
            <SelectUserField id="user" label="User" name="user" required />
          </div>
          <div className="mb-3">
            <SelectAssetField id="asset" label="Asset" name="asset" required />
          </div>
          <div className="mb-3">
            <DateField
              id="assignedDate"
              label="Assigned Date"
              name="assignedDate"
              minDate={new Date()}
              required
            />
          </div>
          <div className="mb-3">
            <TextAreaField
              id="note"
              label="Note"
              name="note"
              maxLength={1200}
              required
            />
          </div>

          <div className="d-flex justify-content-end">
            <Button
              type="submit"
              className="btn btn-danger me-4"
              disabled={!(isValid && dirty) || isSubmitting}
            >
              Save
            </Button>
            <Button
              type="button"
              className="btn btn-secondary"
              onClick={() => navigate(ASSIGNMENTS_LINK)}
            >
              Cancel
            </Button>
          </div>
        </Form>
      )}
    </Formik>
  );
};

export default CreateUpdateAssignmentForm;
