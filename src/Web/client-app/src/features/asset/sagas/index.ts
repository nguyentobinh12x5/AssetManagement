import { takeLatest } from 'redux-saga/effects';
import { getAssets } from '../reducers/asset-slice';
import { getAssetById } from '../reducers/asset-detail-slice';
import { handleGetAssets, handleGetAssetById } from './handles';

export default function* authorizeSagas() {
  yield takeLatest(getAssets.type, handleGetAssets);
  yield takeLatest(getAssetById.type, handleGetAssetById);
}
