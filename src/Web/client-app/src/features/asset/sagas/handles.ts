import { PayloadAction } from '@reduxjs/toolkit';
import { getAssetsRequest } from './requests';
import { call, put } from 'redux-saga/effects';
import { getAssetsFailure, getAssetsSuccess } from '../reducers/asset-slice';

export function* handleGetAssets(action: PayloadAction<any>) {
  try {
    const { data } = yield call(getAssetsRequest, action.payload);
    yield put(getAssetsSuccess(data));
  } catch (error: any) {
    yield put(getAssetsFailure(error.data.detail));
  }
}
