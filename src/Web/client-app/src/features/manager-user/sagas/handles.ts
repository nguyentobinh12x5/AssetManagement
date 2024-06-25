/* eslint-disable @typescript-eslint/no-unused-vars */
import { PayloadAction } from '@reduxjs/toolkit';
import { call, put } from 'redux-saga/effects';
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
} from '../reducers/user-slice';
import {
  deleteUserRequest,
  editUser as editUserRequest,
  getUserById as getUserByIdRequest,
  getUserBySearchTerm,
  getUsers,
  CreateUser as postNewUserRequest,
  getUsersByType,
  getUserTypes,
} from './requests';
import { IUserQuery } from '../interfaces/common/IUserQuery';
import { IUserTypeQuery } from '../interfaces/IUserTypeQuery';
import { IUserSearchQuery } from '../interfaces/IUserSearchQuery';

export function* handleGetUsers(action: PayloadAction<IUserQuery>) {
  const userQuery = action.payload;

  try {
    const { data } = yield call(getUsers, userQuery);
    yield put(setUsers(data));
  } catch (error: any) {
    const msg = error.response.data;
  }
}

export function* handleGetUsersByType(action: PayloadAction<IUserTypeQuery>) {
  const userQuery = action.payload;

  try {
    const { data } = yield call(getUsersByType, userQuery);
    yield put(setUsers(data));
  } catch (error: any) {
    const msg = error.response.data;
  }
}

export function* handleGetUsersBySearchTerm(
  action: PayloadAction<IUserSearchQuery>
) {
  const userQuery = action.payload;

  try {
    const { data } = yield call(getUserBySearchTerm, userQuery);
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
  } catch (error: any) {
    const errorResponse = error.response.data;
    yield put(updateUserError(errorResponse.detail));
    yield;
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

// Handle delete user action
export function* handleDeleteUser(action: PayloadAction<string>) {
  try {
    yield call(deleteUserRequest, action.payload);
    yield put(setDeleteStatus(false));
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
  } catch (error: any) {
    const errorResponse = error.response.data;
    yield put(setCreateUserError(errorResponse.detail));
    yield;
  }
}
