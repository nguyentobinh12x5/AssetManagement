import { PayloadAction } from '@reduxjs/toolkit';
import { call, put } from 'redux-saga/effects';
import {
  createAssetFailure,
  createAssetSuccess,
  getAssetsFailure,
  getAssetsSuccess,
  setAssetCategories,
  setAssetStatuses,
  setAssets,
} from '../reducers/asset-slice';
import { ICreateAssetCommand } from '../interfaces/ICreateAssetCommand';
import {
  createAssetRequest,
  getAssetCategoriesRequest,
  getAssetStatusesRequest,
  getAssetsRequest,
  getAssetByIdRequest,
} from './requests';
import { IAssetQuery } from '../interfaces/common/IAssetQuery';
import { AxiosResponse } from 'axios';
import { IAssetDetail } from '../interfaces/IAssetDetail';
import { ASSETS_LINK } from '../../../constants/pages';
import { navigateTo } from '../../../utils/navigateUtils';
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

export function* handleCreateAsset(action: PayloadAction<ICreateAssetCommand>) {
  try {
    const { data: createdAssetId }: AxiosResponse<number> = yield call(
      createAssetRequest,
      action.payload
    );
    const { data: createdAsset }: AxiosResponse<IAssetDetail> = yield call(
      getAssetByIdRequest,
      createdAssetId
    );
    yield put(createAssetSuccess(createdAsset));
    yield navigateTo(ASSETS_LINK);
  } catch (error: any) {
    const errorMsg = error.data.detail;
    yield put(createAssetFailure(errorMsg));
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
