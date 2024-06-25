import { PayloadAction } from '@reduxjs/toolkit';
import {
  getAssetCategoriesRequest,
  getAssetStatusesRequest,
  getAssetsRequest,
  getAssetByIdRequest
} from './requests';
import { call, put } from 'redux-saga/effects';
import {
  getAssetsFailure,
  getAssetsSuccess,
  setAssetCategories,
  setAssetStatuses,
  setAssets,
} from '../reducers/asset-slice';
import { IAssetQuery } from '../interfaces/common/IAssetQuery';
import { getAssetByIdSuccess } from '../reducers/asset-detail-slice';

export function* handleGetAssets(action: PayloadAction<IAssetQuery>) {
  try {
    const { data } = yield call(getAssetsRequest, action.payload);
    yield put(setAssets(data));
    yield put(getAssetsSuccess(data));
  } catch (error: any) {
    yield put(getAssetsFailure(error.data.detail));
  }
}

export function* handleGetAssetsStatuses() {
  try {
    const { data } = yield call(getAssetStatusesRequest);
    yield put(setAssetStatuses(data));
  } catch (error: any) {
    const errorResponse = error.response.data;
    if (errorResponse.status === 404) {
      yield (window.location.href = '/notfound');
    }
  }
}

export function* handleGetAssetsCategories() {
  try {
    const { data } = yield call(getAssetCategoriesRequest);
    yield put(setAssetCategories(data));
  } catch (error: any) {
    const errorResponse = error.response.data;
    if (errorResponse.status === 404) {
      yield (window.location.href = '/notfound');
    }
  }
}

export function* handleGetAssetById(action: PayloadAction<number>) {
  const id = action.payload;
  try {
    const { data } = yield call(getAssetByIdRequest, id);
    yield put(getAssetByIdSuccess(data));
  } catch (error: any) {
    yield put(getAssetsFailure(error.data.detail));
  }
}