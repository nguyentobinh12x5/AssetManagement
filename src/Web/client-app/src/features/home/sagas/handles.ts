import { PayloadAction } from '@reduxjs/toolkit';
import {
  getMyAssignmentsRequest,
  updateStateAssignmentRequest,
  returningAssignmentRequest,
} from './requests';
import { call, put, select } from 'redux-saga/effects';
import {
  IMyAssignmentQuery,
  IMySelectedAssignment,
} from '../interfaces/IMyAssignment';
import {
  getMyAssignmentsFailure,
  getMyAssignmentsSuccess,
  setUpdateStateAssignmentError,
  updateStateAssignmentSuccess,
  updateReturningSuccess,
} from '../reducers/my-assignment-slice';
import { AssignmentState } from '../../assignment/constants/assignment-state';
import { getMyAssignmentsQuery } from './selectors';

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

export function* handleUpdateStateAssignment(
  action: PayloadAction<IMySelectedAssignment>
) {
  const assignment = action.payload;
  try {
    yield call(updateStateAssignmentRequest, assignment);

    if (assignment.state === AssignmentState.Declined) {
      const query: IMyAssignmentQuery = yield select(getMyAssignmentsQuery);
      const { data } = yield call(getMyAssignmentsRequest, query);
      yield put(getMyAssignmentsSuccess(data));
    } else yield put(updateStateAssignmentSuccess(assignment));
  } catch (error: any) {
    const errorResponse = error.response.data;
    yield put(setUpdateStateAssignmentError(errorResponse.detail));
  }
}

export function* handleReturningAssignment(action: PayloadAction<number>) {
  const assignment = action.payload;
  try {
    yield call(returningAssignmentRequest, assignment);

    yield put(updateReturningSuccess(assignment));
  } catch (error: any) {
    const errorResponse = error.response.data;
    yield put(setUpdateStateAssignmentError(errorResponse.detail));
  }
}
