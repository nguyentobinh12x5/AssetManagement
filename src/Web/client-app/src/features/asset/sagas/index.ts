import { takeLatest } from 'redux-saga/effects';
import { getAssetById } from '../reducers/asset-detail-slice';
import {
  createAsset,
  getAssets,
  getAssetStatuses,
  getAssetCategories,
  editAsset,
  deleteAssets,
} from '../reducers/asset-slice';
import {
  handleCreateAsset,
  handleGetAssets,
  handleGetAssetsCategories,
  handleGetAssetsStatuses,
  handleGetAssetById,
  handleEditAsset,
  handleDeleteAsset,
} from './handles';

export default function* authorizeSagas() {
  yield takeLatest(getAssets.type, handleGetAssets);
  yield takeLatest(createAsset.type, handleCreateAsset);
  yield takeLatest(getAssetStatuses.type, handleGetAssetsStatuses);
  yield takeLatest(getAssetCategories.type, handleGetAssetsCategories);
  yield takeLatest(getAssetById.type, handleGetAssetById);
    yield takeLatest(editAsset.type, handleEditAsset);
    yield takeLatest(deleteAssets.type, handleDeleteAsset);
}
