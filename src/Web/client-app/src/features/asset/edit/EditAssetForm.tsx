import React from "react";
import { Form, Formik } from "formik";
import useEditAsset from "./useEditAsset";
import TextField from "../../../components/formInputs/TextField";
import DateField from "../../../components/formInputs/DateField";
import RadioButtonField from "../../../components/formInputs/RadioButtonField";
import SelectField from "../../../components/formInputs/SelectField";
import TextAreaField from "../../../components/formInputs/TextAreaField";
import Button from "../../../components/Button";
import { EditAssetScheme } from "./edit-asset-scheme";
import { ASSETS_LINK } from "../../../constants/pages";
import { useParams } from "react-router-dom";

const EditAssetForm: React.FC = () => {
  const { id: assetId } = useParams();

  const { assetDetail, handleSubmit, navigate, categories, statuses } =
    useEditAsset(assetId!);

  return assetDetail ? (
    <Formik
      initialValues={{
        id: assetDetail.id,
        category: assetDetail.categoryName,
        installedDate: assetDetail.installedDate,
        name: assetDetail.name,
        specification: assetDetail.specification,
        state: assetDetail.assetStatusName,
      }}
      validationSchema={EditAssetScheme}
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
              disabled={true}
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
              required
            />
          </div>
          <div className="mb-3">
            <RadioButtonField
              id="state"
              label="State"
              name="state"
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
              onClick={() => navigate(ASSETS_LINK)}
            >
              Cancel
            </Button>
          </div>
        </Form>
      )}
    </Formik>
  ) : (
    <></>
  );
};

export default EditAssetForm;
