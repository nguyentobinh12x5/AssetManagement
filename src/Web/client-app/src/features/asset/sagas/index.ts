import { takeLatest } from 'redux-saga/effects';
import { getAssets } from '../reducers/asset-slice';
import { handleGetAssets } from './handles';

export default function* authorizeSagas() {
  yield takeLatest(getAssets.type, handleGetAssets);
}
