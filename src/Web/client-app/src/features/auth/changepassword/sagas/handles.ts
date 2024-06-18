import { PayloadAction } from '@reduxjs/toolkit';
import { call, put } from 'redux-saga/effects';
import {
  changePasswordSuccess,
  changePasswordFailure,
} from '../reducers/change-password-slice';
import { changePassword } from './requests';

export function* handleChangePassword(
  action: PayloadAction<{
    values: { currentPassword: string; newPassword: string };
    actions: any;
    setApiError: any;
  }>
) {
  const { currentPassword, newPassword } = action.payload.values;
  const { setApiError } = action.payload;
  try {
    yield call(changePassword, { currentPassword, newPassword });
    yield put(changePasswordSuccess());
  } catch (error: any) {
    let msg = 'Failed to change password';
    if (error.response?.status === 400) {
      const errorData = error.response.data;
      if (errorData.title === 'Incorrect Password') {
        setApiError({ currentPassword: 'Password is incorrect' });
        msg = 'Password is incorrect';
      } else if (
        errorData.title === 'One or more validation errors occurred.'
      ) {
        setApiError({
          newPassword: errorData.errors?.NewPassword?.join(' ') || msg,
        });
        msg = errorData.errors?.NewPassword?.join(' ') || msg;
      }
    }
    yield put(changePasswordFailure(msg));
  }
}
