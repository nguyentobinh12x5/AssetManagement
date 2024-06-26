import React from "react";
import CreateUpdateAssetForm from "./CreateUpdateAssetForm";

const CreateNewAsset = () => {
  return (
    <div className="asset-form">
      <div className="mb-4">
        <h3 className="primaryColor fw-bold fs-5">Create New Asset</h3>
      </div>
      <CreateUpdateAssetForm />
    </div>
  );
};

export default CreateNewAsset;
