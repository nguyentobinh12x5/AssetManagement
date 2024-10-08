/* eslint-disable @typescript-eslint/no-unused-vars */
import { PayloadAction } from '@reduxjs/toolkit';
import { call, delay, put } from 'redux-saga/effects';
import { IUserCommand } from '../interfaces/IUserCommand';
import {
  setCreateUser,
  setCreateUserError,
  setDeleteStatus,
  setUserById,
  setUserByIdError,
  setUserTypes,
  setUsers,
  updateUser,
  updateUserError,
  removeUser,
} from '../reducers/user-slice';
import {
  deleteUserRequest,
  editUser as editUserRequest,
  getUserById as getUserByIdRequest,
  getUsers,
  CreateUser as postNewUserRequest,
  getUserTypes,
} from './requests';
import { IUserQuery } from '../interfaces/common/IUserQuery';
import { navigateTo } from '../../../utils/navigateUtils';
import { USER_LINK } from '../../../constants/pages';
import { showSuccessToast } from '../../../components/toastify/toast-helper';
import {
  CREATE_USER_SUCCESS,
  EDIT_USER_SUCCESS,
} from '../constants/user-toast-message';

export function* handleGetUsers(action: PayloadAction<IUserQuery>) {
  const userQuery = action.payload;

  try {
    const { data } = yield call(getUsers, userQuery);
    yield put(setUsers(data));
  } catch (error: any) {
    const msg = error.response.data;
  }
}

export function* handleEditUser(action: PayloadAction<IUserCommand>) {
  const user = action.payload;
  try {
    yield call(editUserRequest, user);
    yield put(updateUser(user));
    yield showSuccessToast(EDIT_USER_SUCCESS);
    yield delay(300);
    navigateTo(USER_LINK);
  } catch (error: any) {
    const errorResponse = error.response.data;
    yield put(updateUserError(errorResponse.detail));
  }
}

export function* handleGetUserById(action: PayloadAction<string>) {
  const userId = action.payload;
  try {
    const { data } = yield call(getUserByIdRequest, userId);
    yield put(setUserById(data));
  } catch (error: any) {
    const errorResponse = error.response.data;
    if (errorResponse.status === 404) {
      yield (window.location.href = '/notfound');
    }
    yield put(setUserByIdError(errorResponse.detail));
  }
}

export function* handleGetUserTypes() {
  try {
    const { data } = yield call(getUserTypes);
    yield put(setUserTypes(data));
  } catch (error: any) {
    const errorResponse = error.response.data;
    if (errorResponse.status === 404) {
      yield (window.location.href = '/notfound');
    }
  }
}

export function* handleDeleteUser(action: PayloadAction<string>) {
  try {
    yield call(deleteUserRequest, action.payload);
    yield put(setDeleteStatus(false));
    yield put(removeUser(action.payload));
  } catch (error: any) {
    yield put(setDeleteStatus(false));
  }
}

export function* handleCreateUser(action: PayloadAction<IUserCommand>) {
  const user = action.payload;
  try {
    const { data } = yield call(postNewUserRequest, user);

    const { data: createdUser } = yield call(getUserByIdRequest, data);

    yield put(setCreateUser(createdUser));
    yield showSuccessToast(CREATE_USER_SUCCESS);
    yield delay(300);
    navigateTo(USER_LINK);
  } catch (error: any) {
    const errorResponse = error.response.data;
    yield put(setCreateUserError(errorResponse.detail));
  }
}
