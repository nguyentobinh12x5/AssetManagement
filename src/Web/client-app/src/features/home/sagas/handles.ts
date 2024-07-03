import { PayloadAction } from '@reduxjs/toolkit';
import { getMyAssignmentsRequest } from './requests';
import { call, put } from 'redux-saga/effects';
import { IMyAssignmentQuery } from '../interfaces/IMyAssignment';
import {
  getMyAssignmentsFailure,
  getMyAssignmentsSuccess,
} from '../reducers/my-assignment-slice';

export function* handleGetMyAssignments(
  action: PayloadAction<IMyAssignmentQuery>
) {
  try {
    const { data } = yield call(getMyAssignmentsRequest, action.payload);
    yield put(getMyAssignmentsSuccess(data));
  } catch (error: any) {
    yield put(getMyAssignmentsFailure(error.data.detail));
  }
}
