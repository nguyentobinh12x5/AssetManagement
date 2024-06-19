import { takeLatest } from 'redux-saga/effects';
import {
  changePasswordFirstTime,
  getUserInfo,
  login,
} from '../reducers/auth-slice';
import {
  handleChangePasswordFirstTime,
  handleGetUserInfo,
  handleLogin,
} from './handles';

export default function* authSagas() {
  yield takeLatest(login.type, handleLogin);
  yield takeLatest(getUserInfo.type, handleGetUserInfo);
  yield takeLatest(changePasswordFirstTime.type, handleChangePasswordFirstTime);
}
