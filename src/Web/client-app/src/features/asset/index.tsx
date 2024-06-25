import { Route, Routes } from "react-router-dom";
import { ASSET_LIST } from "./constants/asset-list";
import { CREATE_ASSET_PATH } from "./constants/create-asset";

import { lazy } from "react";

const AssetList = lazy(() => import("./list"));
const CreateNewAsset = lazy(() => import("./create"));

const Assets = () => {
  return (
    <Routes>
      <Route path={ASSET_LIST} element={<AssetList />} />
      <Route path={CREATE_ASSET_PATH} element={<CreateNewAsset />} />
    </Routes>
  );
};

export default Assets;
