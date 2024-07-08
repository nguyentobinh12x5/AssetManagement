import { PayloadAction } from '@reduxjs/toolkit';
import { getReturingsRequest } from './requests';
import { call, put } from 'redux-saga/effects';
import { getReturningsFailure, getReturningsSuccess } from '../reducers/returning-slice';

export function* handleGetReturnings(action: PayloadAction<any>) {
  try {
    const { data } = yield call(getReturingsRequest, action.payload);
    yield put(getReturningsSuccess(data));
  } catch (error: any) {
    yield put(getReturningsFailure(error.data.detail));
  }
}
