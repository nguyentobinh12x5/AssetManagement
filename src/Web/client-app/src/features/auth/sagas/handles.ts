import { PayloadAction } from '@reduxjs/toolkit';
import { call, put } from 'redux-saga/effects';
import { getUserInfo, login } from './requests';
import { loginFail, setAuth, setLogout, setUser } from '../reducers/auth-slice';
import { ILoginCommand } from '../interfaces/ILoginCommand';
import {
  showErrorToast,
  showSuccessToast,
} from '../../../components/toastify/toast-helper';

export function* handleLogin(action: PayloadAction<ILoginCommand>) {
  const loginCommand = action.payload;
  try {
    yield call(login, loginCommand);
    yield put(setAuth());
    yield showSuccessToast('Login success');
  } catch (error: any) {
    const errorResponse = error.response.data;
    if (errorResponse.detail) yield showErrorToast(errorResponse.detail);
    yield put(loginFail(errorResponse.detail));
  }
}

export function* handleGetUserInfo() {
  try {
    const { data } = yield call(getUserInfo);
    console.log(data);
    yield put(setUser(data));
  } catch (error: any) {
    const errorResponse = error.response.data;
    yield put(setLogout());
  }
}
