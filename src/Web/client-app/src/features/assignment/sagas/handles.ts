import { PayloadAction } from '@reduxjs/toolkit';
import {
  createAssignmentRequest,
  deleteAssigmentRequest,
  getAssignmentByIdRequest,
  getAssignmentsRequest,
} from './requests';
import { call, put } from 'redux-saga/effects';
import {
  createAssignmentFailure,
  createAssignmentSuccess,
  deleteAssignmentFailure,
  getAssignmentsFailure,
  getAssignmentsSuccess,
  setDeleteAssignment,
} from '../reducers/assignment-slice';
import {
  getAssignmentByIdFailure,
  getAssignmentByIdSuccess,
} from '../reducers/assignment-detail-slice';
import { ICreateAssignmentCommand } from '../interfaces/ICreateAssignmentCommand';
import { showErrorToast } from '../../../components/toastify/toast-helper';
import { IAssignmentQuery } from '../interfaces/commom/IAssigmentQuery';
import {
  AssignmentState,
  AssignmentStateKey,
} from '../constants/assignment-state';
import { ASSIGNMENTS_LINK } from '../../../constants/pages';
import { navigateTo } from '../../../utils/navigateUtils';

export function* handleGetAssignments(action: PayloadAction<IAssignmentQuery>) {
  try {
    let query = action.payload;
    if (query.state.includes('All')) {
      query = { ...query, state: [] };
    } else {
      query = {
        ...query,
        state: query.state.map((_) =>
          AssignmentState[_ as AssignmentStateKey].toString()
        ),
      };
    }
    const { data } = yield call(getAssignmentsRequest, query);
    yield put(getAssignmentsSuccess(data));
  } catch (error: any) {
    console.error(error);
    yield put(getAssignmentsFailure(error.data.detail));
  }
}
export function* handleGetAssignmentById(action: PayloadAction<any>) {
  try {
    const { data } = yield call(getAssignmentByIdRequest, action.payload);
    yield put(getAssignmentByIdSuccess(data));
  } catch (error: any) {
    yield put(getAssignmentByIdFailure(error.data.detail));
  }
}

export function* handleCreateAssignment(
  action: PayloadAction<ICreateAssignmentCommand>
) {
  try {
    const { data: assignmentId } = yield call(
      createAssignmentRequest,
      action.payload
    );
    const { data } = yield call(getAssignmentByIdRequest, assignmentId);
    yield put(createAssignmentSuccess(data));
    yield navigateTo(ASSIGNMENTS_LINK);
  } catch (error: any) {
    const errorResponse = error.response.data;
    let message = 'Create assignment failed!';
    if (errorResponse.detail) message = errorResponse.detail;

    showErrorToast(message);
  }
}
export function* handleDeleteAssignment(action: PayloadAction<number>) {
    try {
        const id = action.payload;
        yield call(deleteAssigmentRequest, id);
        yield put(setDeleteAssignment(id));
    } catch (error: any) {
        const errorMsg = error.response.data.detail;
        yield put(deleteAssignmentFailure(errorMsg));
    }
}