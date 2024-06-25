import { Form, Formik } from "formik";
import React from "react";
import useCreateAsset from "./useCreateAsset";
import TextField from "../../../components/formInputs/TextField";
import DateField from "../../../components/formInputs/DateField";
import RadioButtonField from "../../../components/formInputs/RadioButtonField";
import { Button, SelectField, TextAreaField } from "../../../components";
import { CreateAssetScheme } from "./create-asset-scheme";
import { ASSETS } from "../../../constants/pages";

const CreateUpdateAssetForm = () => {
  const { initialValues, handleSubmit, navigate, categories, statuses } =
    useCreateAsset();
  return (
    <Formik
      initialValues={initialValues}
      validationSchema={CreateAssetScheme}
      onSubmit={handleSubmit}
    >
      {({ dirty, isValid, isSubmitting }) => (
        <Form>
          <div className="mb-3">
            <TextField
              id="name"
              label="Name"
              name="name"
              type="text"
              maxLength={256}
              required
            />
          </div>
          <div className="mb-3">
            <SelectField
              id="category"
              label="Category"
              name="category"
              options={categories.map((c) => ({
                label: c,
                value: c,
              }))}
              required
            />
          </div>
          <div className="mb-3">
            <TextAreaField
              id="specification"
              label="Specification"
              name="specification"
              maxLength={1200}
              rows={4}
              required
            />
          </div>
          <div className="mb-3">
            <DateField
              id="installedDate"
              label="Installed Date"
              name="installedDate"
              maxDate={new Date()}
            />
          </div>
          <div className="mb-3">
            <RadioButtonField
              id="state"
              label="State"
              name="state"
              value={statuses.length > 0 ? statuses[0] : ""}
              options={statuses.map((st) => ({
                id: st,
                label: st,
                value: st,
              }))}
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
              onClick={() => navigate(ASSETS)}
            >
              Cancel
            </Button>
          </div>
        </Form>
      )}
    </Formik>
  );
};

export default CreateUpdateAssetForm;
