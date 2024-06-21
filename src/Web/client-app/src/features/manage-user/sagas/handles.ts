import { PayloadAction } from '@reduxjs/toolkit';
import { call, put } from 'redux-saga/effects';
import { deleteUserRequest, getUsers } from './requests';
import { setDeleteStatus, setUsers } from '../reducers/user-slice';
import { IUserQuery } from '../interfaces/IUserQuery';

// Handle get users action
export function* handleGetUsers(action: PayloadAction<IUserQuery>) {
  const userQuery = action.payload;

  try {
    const { data } = yield call(getUsers, userQuery);
    yield put(setUsers(data));
  } catch (error: any) {
    const msg = error.response.data;
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
