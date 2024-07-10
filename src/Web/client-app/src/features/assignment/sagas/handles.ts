import { PayloadAction } from '@reduxjs/toolkit';
import {
  createAssignmentRequest,
  deleteAssigmentRequest,
  editAssignmentRequest,
  getAssignmentByIdRequest,
  getAssignmentsRequest,
} from './requests';
import { call, delay, put, select } from 'redux-saga/effects';
import {
  createAssignmentSuccess,
  deleteAssignmentFailure,
  editAssignmentSuccess,
  getAssignmentsFailure,
  getAssignmentsSuccess,
  setDeleteAssignment,
} from '../reducers/assignment-slice';
import {
  getAssignmentByIdFailure,
  getAssignmentByIdSuccess,
} from '../reducers/assignment-detail-slice';
import { ICreateAssignmentCommand } from '../interfaces/ICreateAssignmentCommand';
import {
  showErrorToast,
  showSuccessToast,
} from '../../../components/toastify/toast-helper';
import { IAssignmentQuery } from '../interfaces/commom/IAssigmentQuery';
import {
  AssignmentState,
  AssignmentStateKey,
} from '../constants/assignment-state';
import { ASSIGNMENTS_LINK } from '../../../constants/pages';
import { navigateTo } from '../../../utils/navigateUtils';
import {
  CREATE_ASSIGNMENT_SUCCESS,
  EDIT_ASSIGNMENT_SUCCESS,
} from '../constants/assignment-toast-message';
import { IEditAssignmentCommand } from '../interfaces/IEditAssignmentCommand';
import { getUserQuery } from '../../manager-user/sagas/selectors';
import { IUserQuery } from '../../manager-user/interfaces/common/IUserQuery';
import { setUserQuery } from '../../manager-user/reducers/user-slice';
import { getUsers } from '../../manager-user/sagas/requests';

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

export function* handleGetAssignmentByIdWhenEdit(action: PayloadAction<any>) {
  try {
    // If anyone sees this and wonder "What if the username is administrator@localhost"
    // then thats the seed data problem, this will work on pre-defined UC

    const { data: query } = yield call(
      getAssignmentByIdRequest,
      action.payload
    );
    const userQuery: IUserQuery = yield select(getUserQuery);
    const { data: usersData } = yield call(getUsers, {
      ...userQuery,
      searchTerm: `${query?.assignedTo.slice(0, -1)} ${query?.assignedTo.slice(-1)}`,
    });
    yield put(
      setUserQuery({
        ...userQuery,
        searchTerm: `${usersData?.items[0].fullName.trim()}`,
      })
    );
    yield put(getAssignmentByIdSuccess(query));
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
    yield showSuccessToast(CREATE_ASSIGNMENT_SUCCESS);
    yield delay(500);
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

export function* handleEditAssignment(
  action: PayloadAction<IEditAssignmentCommand>
) {
  try {
    yield call(editAssignmentRequest, action.payload);
    const { data } = yield call(getAssignmentByIdRequest, +action.payload.id);
    yield put(editAssignmentSuccess(data));
    yield showSuccessToast(EDIT_ASSIGNMENT_SUCCESS);
    yield delay(500);
    yield navigateTo(ASSIGNMENTS_LINK);
  } catch (error: any) {
    const errorResponse = error.response.data;
    let message = 'Edit assignment failed!';
    if (errorResponse.detail) message = errorResponse.detail;

    showErrorToast(message);
  }
}
