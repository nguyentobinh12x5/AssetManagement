import { PayloadAction } from '@reduxjs/toolkit';
import { call, put } from 'redux-saga/effects';
import {
  changePasswordFirstTime,
  getUserInfo as getUserInfoRequest,
  login as loginRequest,
  logout as logoutRequest,
} from './requests';
import {
  changePasswordFirstTimeSuccess,
  loginFail,
  setAuth,
  setLogout,
  setUser,
} from '../reducers/auth-slice';
import { ILoginCommand } from '../interfaces/ILoginCommand';
import { showErrorToast } from '../../../components/toastify/toast-helper';
import { IChangePasswordFirstTimeCommand } from '../interfaces/IChangePasswordFirstTimeCommand';
import { AxiosResponse } from 'axios';
import { IUserInfo } from '../interfaces/IUserInfo';

export function* handleLogin(action: PayloadAction<ILoginCommand>) {
  const loginCommand = action.payload;
  try {
    yield call(loginRequest, loginCommand);
    yield put(setAuth());
    yield call(handleGetUserInfo);
  } catch (error: any) {
    const errorResponse = error.response.data;
    if (errorResponse.detail) yield showErrorToast(errorResponse.detail);
    yield put(loginFail(errorResponse.detail));
  }
}

export function* handleGetUserInfo() {
  try {
    const { data }: AxiosResponse<IUserInfo> = yield call(getUserInfoRequest);
    yield put(setUser(data));
  } catch (error: any) {
    const errorResponse = error.response.data;
    yield put(setLogout());
  }
}

export function* handleChangePasswordFirstTime(
  action: PayloadAction<IChangePasswordFirstTimeCommand>
) {
  try {
    yield call(changePasswordFirstTime, action.payload);
    yield put(changePasswordFirstTimeSuccess());
  } catch (error: any) {
    const errorResponse = error.response.data;
    if (errorResponse.detail) yield showErrorToast(errorResponse.detail);
  }
}

export function* handleLogout() {
  try {
    yield call(logoutRequest);
    yield put(setLogout());
  } catch (error: any) {
    const errorResponse = error.response.data;
    if (errorResponse.detail) yield showErrorToast(errorResponse.detail);
  }
}
