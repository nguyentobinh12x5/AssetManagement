import { PayloadAction } from '@reduxjs/toolkit';
import { call, put } from 'redux-saga/effects';
import { IUser } from '../interfaces/IUser';
import {
    setCreateUser,
  setCreateUserError,
  setUserById,
  setUserByIdError,
  setUsers,
  updateUser,
  updateUserError,
} from '../reducers/user-slice';
import {
  editUser as editUserRequest,
  getUserById as getUserByIdRequest,
  getUsers,
  CreateUser as postNewUserRequest
} from './requests';
import { IUserQuery } from '../interfaces/IUserQuery';

export function* handleGetUsers(action: PayloadAction<IUserQuery>) {
  const userQuery = action.payload;

  try {
    const { data } = yield call(getUsers, userQuery);
    yield put(setUsers(data));
  } catch (error: any) {
    const msg = error.response.data;
  }
}
export function* handleEditUser(action: PayloadAction<IUser>) {
  const user = action.payload;
  try {
    const { data } = yield call(editUserRequest, user);
    yield put(updateUser(data));
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
export function* handleCreateUser(action: PayloadAction<IUser>) {
    const user = action.payload;
    try {
        const { data } = yield call(postNewUserRequest, user);
        yield put(setCreateUser(data));
    } catch (error: any) {
        const errorResponse = error.response.data;
        yield put(setCreateUserError(errorResponse.detail));
        yield;
    }
}