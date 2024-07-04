import { PayloadAction } from '@reduxjs/toolkit';
import { call, delay, put } from 'redux-saga/effects';
import {
  createAssetFailure,
  createAssetSuccess,
  deleteAssetFailure,
  setDeleteAsset,
  editAssetFailure,
  editAssetSuccess,
  getAssetsFailure,
  getAssetsSuccess,
  setAssetCategories,
  setAssetStatuses,
} from '../reducers/asset-slice';
import { ICreateAssetCommand } from '../interfaces/ICreateAssetCommand';
import { IEditAssetCommand } from '../interfaces/IEditAssetCommand';
import {
  createAssetRequest,
  editAssetRequest,
  getAssetCategoriesRequest,
  getAssetStatusesRequest,
  getAssetsRequest,
  getAssetByIdRequest,
  deleteAssetRequest,
} from './requests';
import { IAssetQuery } from '../interfaces/common/IAssetQuery';
import { AxiosResponse } from 'axios';
import { IAssetDetail } from '../interfaces/IAssetDetail';
import { ASSETS_LINK } from '../../../constants/pages';
import { navigateTo } from '../../../utils/navigateUtils';
import { getAssetByIdSuccess } from '../reducers/asset-detail-slice';
import { showSuccessToast } from '../../../components/toastify/toast-helper';
import {
  CREATE_ASSET_SUCCESS,
  EDIT_ASSET_SUCCESS,
} from '../constants/asset-toast-message';

export function* handleGetAssets(action: PayloadAction<IAssetQuery>) {
  let query = action.payload;
  if (query.assetStatus.includes('All'))
    query = {
      ...query,
      assetStatus: [],
    };

  if (query.category.includes('All'))
    query = {
      ...query,
      category: [],
    };

  try {
    const { data } = yield call(getAssetsRequest, query);
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
    yield showSuccessToast(CREATE_ASSET_SUCCESS);
    yield delay(500);
    yield navigateTo(ASSETS_LINK);
  } catch (error: any) {
    const errorMsg = error.data.detail;
    yield put(createAssetFailure(errorMsg));
  }
}

export function* handleEditAsset(action: PayloadAction<IEditAssetCommand>) {
  try {
    yield call(editAssetRequest, action.payload);
    const { data: updateAsset }: AxiosResponse<IAssetDetail> = yield call(
      getAssetByIdRequest,
      action.payload.id
    );
    yield put(editAssetSuccess(updateAsset));
    yield showSuccessToast(EDIT_ASSET_SUCCESS);
    yield delay(500);
    yield navigateTo(ASSETS_LINK);
  } catch (error: any) {
    const errorMsg = error.data.detail;
    yield put(editAssetFailure(errorMsg));
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

export function* handleDeleteAsset(action: PayloadAction<number>) {
  try {
    const id = action.payload;
    yield call(deleteAssetRequest, id);
    yield put(setDeleteAsset(id));
  } catch (error: any) {
    const errorMsg = error.response.data.detail;
    yield put(deleteAssetFailure(errorMsg));
  }
}
