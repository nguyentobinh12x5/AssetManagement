import { takeLatest } from 'redux-saga/effects';
import {
  getAssets,
  getAssetStatuses,
  getAssetCategories,
} from '../reducers/asset-slice';
import {
  handleGetAssets,
  handleGetAssetsCategories,
  handleGetAssetsStatuses,
} from './handles';

export default function* authorizeSagas() {
  yield takeLatest(getAssets.type, handleGetAssets);
  yield takeLatest(getAssetStatuses.type, handleGetAssetsStatuses);
  yield takeLatest(getAssetCategories.type, handleGetAssetsCategories);
}
