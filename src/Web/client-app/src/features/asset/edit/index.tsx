import React from "react";
import EditAssetForm from "./EditAssetForm";

const EditAsset = () => {
  return (
    <div className="asset-form">
      <div className="mb-4">
        <h3 className="primaryColor fw-bold fs-5">Edit Asset</h3>
      </div>
      <EditAssetForm />
    </div>
  );
};

export default EditAsset;
